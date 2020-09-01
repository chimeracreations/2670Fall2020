using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 movement;
    public float gravity = 0.13f;
    public float moveSpeed = 4f;
    public float fastMoveSpeed;
    public float jumpForce = 12f;
    private int jumpCount = 0;
    public int maxJumpCount = 2;
    public int jumpCountMax;
    public float rotateSpeed = 3f;
    private Vector3 rotateMovement;
    public bool testGrounded;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {  
        movement.y -= gravity;

        if (controller.isGrounded)
        {
            testGrounded = true;
            movement.y = -gravity * 3;
            jumpCount = 0;
        }
          else testGrounded = false;

        if (Input.GetButtonDown("Jump") && (jumpCount < maxJumpCount))
        {
            movement.y = jumpForce;
            jumpCount++;
        }


        movement.x = Input.GetAxis("Horizontal") * moveSpeed;
        movement.z = Input.GetAxis("Vertical") * moveSpeed;
        
        

        movement = transform.TransformDirection(movement);
        controller.Move(movement*Time.deltaTime);
    }

  
}