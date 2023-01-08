using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletForce;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    }

    private void Update() {
        Destroy(this.gameObject, 5);
    }
}
