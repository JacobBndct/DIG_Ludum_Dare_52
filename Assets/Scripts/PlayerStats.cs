using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [SerializeField] private float health;
    private bool isAlive;
    public void Damage(float dmg) {
        health -= dmg;
    }

    private void Update() {
        if (health < 0) {
            isAlive = false;
            Destroy(this.gameObject);
        }
        else {
            isAlive = true;
        }
    }
}
