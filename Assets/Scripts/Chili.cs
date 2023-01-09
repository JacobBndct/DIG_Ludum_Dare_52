using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chili : MonoBehaviour
{
    public int scoreValue = 5000;
    public float specialValue = 0.10f;
    public AudioClip pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ScoreManager.Instance.AddScore(scoreValue);
            SpecialPointsManager.Instance.AddSpecialPoints(specialValue);

            // same deal as in LittleGreenDude
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            AudioSource playerAudio = player.GetComponent<AudioSource>();



            playerAudio.PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}
