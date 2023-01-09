using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGreenDude : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject playerReference;
    [SerializeField] private float speed;

    public GameObject explosion;

    public bool canMove;

    public int pointWorth;

    public int attackDmg;

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
        if (canMove && playerReference != null)
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

    /*
    private IEnumerator OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("player in range");
            other.gameObject.GetComponent<PlayerStats>().Damage(attackDmg);
        }
    }
    */
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player in range");
            other.gameObject.GetComponent<PlayerStats>().Damage(attackDmg);
        }
    }

    // Called upon DEATH
    public void Death()
    {
        // we can do some more fancy shit here later
        ScoreManager.Instance.AddScore(pointWorth);

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);
        Debug.Log("DEATH");
    }
}
