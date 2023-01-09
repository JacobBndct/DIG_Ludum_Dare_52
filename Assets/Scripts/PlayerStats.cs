using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float health;
    private bool isAlive;
    public void Damage(float dmg)
    {
        Debug.Log("Damage " + health);
        health -= dmg;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Death");
            isAlive = false;
            Destroy(this.gameObject);
        }
        else
        {
            isAlive = true;
        }
    }
}