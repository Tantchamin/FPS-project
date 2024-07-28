using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform orientation;
    private Rigidbody playerRigidbody;
    private Vector3 moveDirection;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        MoveCharacter();

    }

    void FixedUpdate()
    {
    }

    void MoveCharacter()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        //playerRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);

        moveDirection = transform.right * xInput + transform.forward * zInput;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);


    }
}
