using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody playerRigidbody;
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

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();    
        foreach(Gun gun in playerStatus.gunInventory)
        {
            gun.fpsCamera = fpsCamera;
        }
    }

    void Update()
    {
        CharacterGravity();
        MoveCharacter();

        if (Input.GetButtonDown("Fire1"))
        {
            playerStatus.gunInventory[playerStatus.equipedGun].Shoot();
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
