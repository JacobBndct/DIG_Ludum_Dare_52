using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _playerRigidbody;

    private Vector2 playerDirection;
    private Vector2 playerAim;

    public GameObject playerGun;

    [Header("Movement")]
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] InputHandler _input;

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get Inputs
        //float directionX = Input.GetAxisRaw("Horizontal");
        //float directionY = Input.GetAxisRaw("Vertical");

        //playerDirection = new Vector2(directionX, directionY).normalized;

        
    }

    private void FixedUpdate()
    {
        Move();
        Aim();

        _playerRigidbody.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);



        float angle = Mathf.Atan2(playerAim.x, -playerAim.y) * Mathf.Rad2Deg;
        playerGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //playerGun.transform.Rotate(playerAim);
    }

    public void Move()
    {
        playerDirection = _input.MoveInput.normalized;
    }

    public void Aim()
    {
        playerAim = _input.LookInput;
        
    }

    public void Fire()
    {
        Debug.Log("Fire!");
    }
}