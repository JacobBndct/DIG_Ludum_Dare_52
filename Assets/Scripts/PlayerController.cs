using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _playerRigidbody;

    private Vector2 playerDirection;
    private Vector2 playerAim;
    private Vector2 playerMouseAim;

    [Header("HEALT")]
    public int playerHealth;
    public bool isInvulnerable = false;
    public bool canShoot = false;
    public float fireCoolDown;

    public GameObject playerGun;

    public GameObject plant;

    private GameObject overlapPlant;

    public bool canPlant = true;
    public bool canHarvest = false;

    AudioSource audioSource;

    public AudioClip harvestClip;
    public AudioClip fullHarvestClip;

    Component gun;

    [SerializeField] private GameObject pelletShooter;
    
    
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] InputHandler _input;

    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    IEnumerator CoolDown(float coolDown)
    {
        canShoot = false;
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }

    void Update()
    {
        // Get Inputs
        //float directionX = Input.GetAxisRaw("Horizontal");
        //float directionY = Input.GetAxisRaw("Vertical");

        //playerDirection = new Vector2(directionX, directionY).normalized;
        Shoot();
        Move();

        if (Gamepad.current == null)
        {
            MouseAim();
        } else
        {
            Aim();
        }

        Plant();
        Special();
    }

    private void FixedUpdate()
    {
        _playerRigidbody.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
        
        // COMMENTED OUT GAMEPAD CONTROLS
        // playerGun.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(playerAim.y, playerAim.x)));

        if (Gamepad.current == null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(playerMouseAim);
            playerGun.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(mousePosition.y - playerGun.transform.position.y, mousePosition.x - playerGun.transform.position.x)));
        } else
        {
            playerGun.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(playerAim.y, playerAim.x)));
        }


        Debug.Log(playerAim);
        
    }

    public void Move()
    {
        playerDirection = _input.MoveInput.normalized;
    }

    public void Aim()
    {
        playerAim = _input.LookInput;
        
    }

    public void MouseAim()
    {
        playerMouseAim = _input.MouseLookInput;
    }
    
    public void Shoot()
    {
        if (_input.FireInput.action.WasPressedThisFrame() && canShoot) {
            Debug.Log("shooting");
            StartCoroutine(CoolDown(fireCoolDown));
            pelletShooter.GetComponent<Gun>().Shoot();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant" && !canHarvest)
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

    // by the POWER OF OHIO
    public void OhioDeathBlast()
    {
        SpecialPointsManager.Instance.SetSpecialPoints(0f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        audioSource.Play();

        for (int i = 0; i < enemies.Length; i++) 
        { 
            LittleGreenDude greenie = enemies[i].GetComponent<LittleGreenDude>();
            greenie.Death();
        }

    }

    public void Special()
    {
        if (_input.SpecialInput.action.WasPerformedThisFrame())
        {
            if (SpecialPointsManager.Instance.GetSpecialPoints() > 0.90f)
            {
                OhioDeathBlast();
            } else
            {
                // maybe play a "nuh-uh not happenin" sound
                Debug.Log("You need more points!");
            }
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
                
                Plant tempPlant = overlapPlant.GetComponent<Plant>();
                

                SpecialPointsManager.Instance.AddSpecialPoints(tempPlant.SpecialPointAmount);

                if (SpecialPointsManager.Instance.GetSpecialPoints() > 0.90f)
                {
                    audioSource.PlayOneShot(fullHarvestClip);
                } else
                {
                    audioSource.PlayOneShot(harvestClip);
                }

                Destroy(overlapPlant);
            } else
            {
                GameObject p = Instantiate(plant, transform.position, transform.rotation);
            }

        } 
    }
}