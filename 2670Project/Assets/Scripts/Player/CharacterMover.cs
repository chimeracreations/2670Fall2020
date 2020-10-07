using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 movement;
    public float gravity = 25f;
    public float jumpForce = 12f;
    private int jumpCount = 0;
    public int maxJumpCount = 2;
    public bool canJump = true;
    public float moveSpeed = 4f;
    public float fastMoveSpeed = 6f;
    public float dashMoveSpeed = 12f;
    public float recoveryMoveSpeed = 2.5f;
    private float rotateAngle;
    private float deltaAngle;
    public float rotateSpeed = 2.8f;
    public float dashCooldown = 0.3f;
    public float dashRest = 0.8f;
    private float dashCount = 0f;
    private float dashRestCount = 0f;
    private bool canDash = true;
    private Animator animator;
    public float attackPause = 2f;
    private float attackPauseCount;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        attackPauseCount = attackPause;
    }
    
    void Update()
    {   
        //Constant increasing downward movement for gravity  
        movement.y -= gravity * Time.deltaTime;
        
        //JUMP
        //Keep downward movement from going out of control by reseting gravity but with extra umph to keep grounded. Reset jump count when hits the ground 
        if (controller.isGrounded)
        {
            movement.y = -3;
            jumpCount = 0;
        }

        //incase you fall from a cliff without jumping first
        else if (controller.isGrounded == false && jumpCount < 1)
            jumpCount++;

        //Jump up as many times as jumpCount is set to
        if (Input.GetButtonDown("Jump") && (jumpCount < maxJumpCount) && canJump)
        {
            movement.y = jumpForce;
            jumpCount++;
        }
       
        //MOVEMENT
        //If canDash is true and Fire2 is pressed, sets dashCount to a positive number to allow the next if statement to trigger
        if (Input.GetButtonDown("Fire2") && canDash) 
        {
            dashCount = dashCount + 1 * Time.deltaTime;
        }

        //Moves the character at dash speed for a set time. dashCooldown is not 1 value equals 1 second but should run off real time
        else if (dashCount > 0 && dashCount < dashCooldown)
        {
            movement.x = Input.GetAxis("Horizontal") * dashMoveSpeed;
            movement.z = Input.GetAxis("Vertical") * dashMoveSpeed;
            dashCount = dashCount + 1 * Time.deltaTime;
        }

        //Resets dashCount and canDash once dashcount reaches the dashCooldown
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
        //Fast movement
        else if (Input.GetButton("Fire3") && canDash)
        {
            movement.x = Input.GetAxis("Horizontal") * fastMoveSpeed;
            movement.z = Input.GetAxis("Vertical") * fastMoveSpeed;
        }
        //Regular movement
        else if (canDash)
        {
            movement.x = Input.GetAxis("Horizontal") * moveSpeed;
            movement.z = Input.GetAxis("Vertical") * moveSpeed;
        }

        //ROTATE
        //Attack animation freezes rotation until idle animation resumes
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerIdle"))
        {    
            float angleOne = rotateAngle;
            //Player rotation based off input
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
            {
                rotateAngle = Mathf.LerpAngle(rotateAngle, 45, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
            {
                rotateAngle = Mathf.LerpAngle(rotateAngle, -45, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
            {
                rotateAngle = Mathf.LerpAngle(rotateAngle, 135, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
            {
                rotateAngle = Mathf.LerpAngle(rotateAngle, -135, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0)
            {
                rotateAngle = Mathf.LerpAngle(rotateAngle, 90, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
            {
                rotateAngle = Mathf.LerpAngle(rotateAngle, -90, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0)
            {
                rotateAngle = Mathf.LerpAngle(rotateAngle, 0, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0)
            {
                rotateAngle = Mathf.LerpAngle(rotateAngle, 180, (rotateSpeed * Time.deltaTime));
            }

            //Get the deltaAngle by subracting the before and after rotateAngle
            float angleTwo = rotateAngle;
            deltaAngle = angleOne - angleTwo;
            //Attack pause counter
            attackPauseCount = attackPauseCount + (attackPauseCount * Time.deltaTime);
        }

        //ATTACK
        //Attack animation after attack pause count 
        if (Input.GetButtonDown("Fire1") && deltaAngle <= 0 && attackPauseCount > attackPause)
        {
            animator.SetTrigger("playerAttackR");
            attackPauseCount = 1f;
        }
        else if (Input.GetButtonDown("Fire1") && deltaAngle > 0 && attackPauseCount > attackPause)
        {
            animator.SetTrigger("playerAttackL");
            attackPauseCount = 1f;
        }

        //THE WORK
        //Code that takes from above and does the work
        transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, rotateAngle, 0)); 
        movement = transform.TransformDirection(movement);
        controller.Move(movement * Time.deltaTime);
    }

}