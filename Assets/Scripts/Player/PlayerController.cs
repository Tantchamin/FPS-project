using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        Vector3 moveVelocity = moveDirection * moveSpeed;
        playerRigidbody.velocity = new Vector3(moveVelocity.x, playerRigidbody.velocity.y, moveVelocity.z);

        //playerRigidbody.velocity = (transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }
}
