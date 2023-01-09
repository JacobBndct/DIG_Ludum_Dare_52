using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGreenDude : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject playerReference;
    [SerializeField] private float speed;

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

    void Update()
    {
        if (canMove)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector2.MoveTowards(transform.position, playerReference.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pellet")
        {
            Destroy(collision.gameObject);
            Death();
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
