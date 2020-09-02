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
    public float fastMoveSpeed = 6f;
    public float dashMoveSpeed = 12f;
    public float recoveryMoveSpeed = 2.5f;
    public float jumpForce = 12f;
    private int jumpCount = 0;
    public int maxJumpCount = 2;
    public float rotateSpeed = 3f;
    private Vector3 rotateMovement;
    public bool canJump = true;
    public float dashCooldown = 0.3f;
    public float dashRest = 0.8f;
    private float dashCount;
    private float dashRestCount;
    private bool canDash = true;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        dashCount = 0;
        dashRestCount = 0;
    }
    
    void Update()
    {  
        movement.y -= gravity;

        if (controller.isGrounded)
        {
            movement.y = -gravity * 3;
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && (jumpCount < maxJumpCount) && canJump)
        {
            movement.y = jumpForce;
            jumpCount++;
        }
       
        if (Input.GetButtonDown("Fire2") && canDash) 
        {
            dashCount = dashCount + 1 * Time.deltaTime;
        }

        else if (dashCount > 0 && dashCount < dashCooldown)
        {
            movement.x = Input.GetAxis("Horizontal") * dashMoveSpeed;
            movement.z = Input.GetAxis("Vertical") * dashMoveSpeed;
            dashCount = dashCount + 1 * Time.deltaTime;
        }

        else if (dashCount >= dashCooldown)
        {
            dashCount = 0;
            canDash = false;         
        }

        else if (canDash == false)
        {
            movement.x = Input.GetAxis("Horizontal") * recoveryMoveSpeed;
            movement.z = Input.GetAxis("Vertical") * recoveryMoveSpeed;
            dashRestCount = dashRestCount + 1 * Time.deltaTime;
            if (dashRestCount >= dashRest)
            {
                canDash = true;
                dashRestCount = 0;
            }
        }

        else if (Input.GetButton("Fire3") && canDash)
        {
            movement.x = Input.GetAxis("Horizontal") * fastMoveSpeed;
            movement.z = Input.GetAxis("Vertical") * fastMoveSpeed;
        }

        else if (canDash)
        {
            movement.x = Input.GetAxis("Horizontal") * moveSpeed;
            movement.z = Input.GetAxis("Vertical") * moveSpeed;
        }
        
        

        movement = transform.TransformDirection(movement);
        controller.Move(movement*Time.deltaTime);
    }

  
}