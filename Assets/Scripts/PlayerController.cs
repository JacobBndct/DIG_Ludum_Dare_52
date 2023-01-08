using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _playerRigidbody;

    private Vector2 playerDirection;
    private Vector2 playerAim;

    public GameObject playerGun;

    Component gun;

    [SerializeField] private GameObject pelletShooter;
    [SerializeField] private float fireCoolDown;
    [SerializeField] private bool canShoot;
    
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] InputHandler _input;
    void Start()
    {
        canShoot = true;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        
    }

    void Update() {
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

    IEnumerator CoolDown(float coolDown) {
        canShoot = false;
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }
    
    private void Shoot() {
        if (_input.FireInput.action.WasPressedThisFrame() && canShoot) {
            Debug.Log("shooting");
            pelletShooter.GetComponent<Gun>().Shoot();
            StartCoroutine(CoolDown(fireCoolDown));
        }
        
        
    }
}