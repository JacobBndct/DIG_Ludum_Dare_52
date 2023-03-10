using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float health;
    private bool isAlive = true;
    public bool invulnerable = false;

    public float invulTime = 1f;

    public float tint = 1f;
    public float tintModifier = 0.40f;

    SpriteRenderer sprite;

    AudioSource audioSource;
    public AudioClip hitSound;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(float dmg)
    {
        if (!invulnerable)
        {
            tint -= tintModifier;

            audioSource.PlayOneShot(hitSound);
            sprite.color = new Color(1, 1, 1, 0.5f);
            Debug.Log("Damage " + health);
            health -= dmg;
            invulnerable = true;
            // start coroutine
            StartCoroutine(invulnerability());
        }


        if (health <= 0)
        {
            Debug.Log("Death");
            isAlive = false;
            //Singletons are yucky

            SpecialPointsManager.Instance.SetSpecialPoints(0.5f);
            CustomSceneManager.Instance.LoadEndScene();

            Destroy(this.gameObject);
        }
    }

    IEnumerator invulnerability()
    {
        Debug.Log("Start invul");
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
        sprite.color = new Color(1, tint, tint, 1f);
        Debug.Log("End invul");
    }
}
