using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 1f;
    private Vector3 moveDirection;

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.3f;
    private const float gravity = -9.8f;
    private Vector3 playerVelocity;
    bool isGrounded = false;

    [Header("PlayerStatus")]
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private Camera fpsCamera;

    GameManager gameManager;
    SoundManager soundManager;
    Gun equppingGun;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        soundManager = SoundManager.GetInstance();
        foreach(Gun gun in playerStatus.gunInventory)
        {
            gun.fpsCamera = fpsCamera;
        }
        equppingGun = playerStatus.gunInventory[playerStatus.equipedGun];
    }

    void Update()
    {
        if (gameManager.isPause) return;
        CharacterGravity();
        MoveCharacter();

        if (Input.GetButton("Fire1") && Time.time >= equppingGun.nextTimeToFire)
        {
            equppingGun.nextTimeToFire = Time.time + 1f / equppingGun.fireRate;
            equppingGun.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            equppingGun.Reload();
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            soundManager.PlaySound("Button", false);
            gameManager.PauseGame(true);
        }

    }

    private void CharacterGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
    }

    void MoveCharacter()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        moveDirection = transform.right * xInput + transform.forward * zInput;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;

        controller.Move(playerVelocity);
    }

    
}
