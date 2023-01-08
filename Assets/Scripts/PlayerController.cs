using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _playerRigidbody;

    private Vector2 playerDirection;
    private Vector2 playerAim;

    public GameObject playerGun;

    public GameObject plant;

    private GameObject overlapPlant;

    public bool canPlant = true;
    public bool canHarvest = false;

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

        Plant();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            canHarvest = true;
            overlapPlant = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            canHarvest = false;
        }
    }

    public void Plant()
    {
        if (_input.PlantInput.action.WasPressedThisFrame())
        {
            Debug.Log("PLANT");
            // set out an event to say 'hey, we just planted' or something like that lol

            if (canHarvest)
            {
                Destroy(overlapPlant);
            } else
            {
                GameObject p = Instantiate(plant, transform.position, transform.rotation);
            }

        } 
    }
}