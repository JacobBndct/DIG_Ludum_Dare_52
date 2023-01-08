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

    Component gun;

    [SerializeField] private GameObject pelletShooter;
    
    
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
        Shoot();
        Move();
        Aim();
    }

    private void FixedUpdate()
    {
        

        _playerRigidbody.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(playerAim);
        
        playerGun.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(mousePosition.y - playerGun.transform.position.y, mousePosition.x - playerGun.transform.position.x)));
        
    }

    public void Move()
    {
        playerDirection = _input.MoveInput.normalized;
    }

    public void Aim()
    {
        playerAim = _input.LookInput;
        
    }
    
    public void Shoot()
    {
        if (_input.FireInput.action.WasPressedThisFrame()) {
            Debug.Log("shooting");
            pelletShooter.GetComponent<Gun>().Shoot();
        }
        
    }
}