using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LittleGreenDude : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject playerReference;
    [SerializeField] private float speed;
    [SerializeField] private float attackDmg;

    public bool canMove;

    Animator an;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        playerReference = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(playerReference);
    }

    private void Start()
    {
        StartCoroutine(countDown());
    }

    IEnumerator countDown()
    {
        float seconds;

        seconds = UnityEngine.Random.Range(3, 7);
        
        print("Start waiting for: " + seconds);
        yield return new WaitForSeconds(seconds);
        canMove = !canMove;

        if (canMove)
        {
            an.speed = 1;
        } else
        {
            an.speed = 0;
        }
        StartCoroutine(countDown());
    }

    private void Update() {
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(playerReference.transform.position.y - transform.position.y, playerReference.transform.position.x - transform.position.x)));   
    }

    private void FixedUpdate() {
        rb.AddForce(transform.right * speed);
    }

    // void Update()
    // {
    //     if (canMove)
    //     {
    //         var step = speed * Time.deltaTime; // calculate distance to move
    //         transform.position = Vector2.MoveTowards(transform.position, playerReference.transform.position, step);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Pellet")) {
            Debug.Log("pellet hit");
            Destroy(transform.parent.gameObject);
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("player in range");
            other.gameObject.GetComponent<PlayerStats>().Damage(attackDmg);
        }
    }

    // Called upon DEATH
    void Death()
    {
        // we can do some more fancy shit here later
        Destroy(gameObject);
        Debug.Log("DEATH");
    }
}
