using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public Vector3 movement;
    [HideInInspector] public bool canControl = true;
    public float gravityForce = 25f;
    public float jumpForce = 12f;
    private int jumpCount = 0;
    public int maxJumpCount = 2;
    public bool canJump = true;
    public float moveSpeed = 4f;
    public float fastMoveSpeed = 6f;
    public float dashMoveSpeed = 12f;
    public float recoveryMoveSpeed = 2.5f;
    public float diagonalRestraint = 0.9f;
    private float rotateAngle;
    private float deltaAngle;
    public float rotateSpeed = 2.8f;
    public float dashCooldown = 0.3f;
    public float dashRest = 0.8f;
    private float dashCount = 0f;
    private float dashRestCount = 0f;
    private bool canDash = true;
    private TrailRenderer dashTrail;
    private Animator animator;
    public float attackPause = 1.15f;
    private float attackPauseCount;
    private GameObject tailStink;
    private TrailRenderer tailTrail;
    public float TailEmit = 0.8f;
    private float tailEmitCount;
    public bool madeNoise;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    private float knockbackDuration = .4f;
    private float immuneDuration = 1.9f;
    private Vector3 pushDirection;
    [HideInInspector] public bool isKnockbacked = false;
    private GameObject playerModel;
    private GameObject tail;
    private Transform tailPosition;
    private TrailRenderer backTrail;
    private Collider tailCol;
    private Renderer playerColor;
    private Renderer playerColor2;
    private Color color;
    public GameObject bomb;
    public float bombCooldown = 5f;
    private float bombCooldownCount = 5f;
    public float wallCooldown = 8f;
    private float wallCooldownCount = 8f;
    public float wallLength = 1.5f;
    public GameObject wall;
    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        tail = GameObject.FindGameObjectWithTag("Tail");
        tailPosition = tail.transform;
        backTrail = tail.GetComponent<TrailRenderer>();
        backTrail.emitting = false;
        playerColor = playerModel.GetComponent<Renderer>();
        playerColor2 = tail.GetComponentInParent<Renderer>();
        attackPauseCount = attackPause;
        dashTrail = GetComponent<TrailRenderer>();
        tailStink = GameObject.FindGameObjectWithTag("TailStink");
        tailTrail = tailStink.GetComponent<TrailRenderer>();
        dashTrail.emitting = false;
        tailTrail.emitting = false;
        tailEmitCount = 1;
        tailCol = tailStink.GetComponent<Collider>();
        tailCol.enabled = false;
    }
    
    void Update()
    {   
        madeNoise = false;
        if (canControl == true)
        {
            //Constant increasing downward movement for gravity  
            movement.y -= gravityForce * Time.deltaTime;
            
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
                dashTrail.emitting = true;
                madeNoise = true;
            }

            //Moves the character at dash speed for a set time. dashCooldown is not 1 value equals 1 second but should run off real time
            else if (dashCount > 0 && dashCount < dashCooldown)
            {
                movement.x = Input.GetAxis("Horizontal") * dashMoveSpeed;
                movement.z = Input.GetAxis("Vertical") * dashMoveSpeed;
                dashCount = dashCount + 1 * Time.deltaTime;
                madeNoise = true;
            }

            //Resets dashCount and canDash once dashcount reaches the dashCooldown
            else if (dashCount >= dashCooldown)
            {
                dashCount = 0;
                canDash = false;  
                dashTrail.emitting = false;       
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
                madeNoise = true;
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
                    movement.x = movement.x * diagonalRestraint;
                    movement.z = movement.z * diagonalRestraint;
                }
                if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, -45, (rotateSpeed * Time.deltaTime));
                    movement.x = movement.x * diagonalRestraint;
                    movement.z = movement.z * diagonalRestraint;
                }
                if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, 135, (rotateSpeed * Time.deltaTime));
                    movement.x = movement.x * diagonalRestraint;
                    movement.z = movement.z * diagonalRestraint;
                }
                if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, -135, (rotateSpeed * Time.deltaTime));
                    movement.x = movement.x * diagonalRestraint;
                    movement.z = movement.z * diagonalRestraint;
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
                tailCol.enabled = true;
                tailEmitCount = 0;
                animator.SetTrigger("playerAttackR");
                attackPauseCount = 1f;

            }
            else if (Input.GetButtonDown("Fire1") && deltaAngle > 0 && attackPauseCount > attackPause)
            {
                tailCol.enabled = true;
                tailEmitCount = 0;
                animator.SetTrigger("playerAttackL");
                attackPauseCount = 1f;

            }
            tailEmitCount = tailEmitCount + 1 * Time.deltaTime;
            if (tailEmitCount < TailEmit)
            {
                tailTrail.emitting = true;
                madeNoise = true;
            }
            else 
            {
                tailTrail.emitting = false;
                tailCol.enabled = false;
            }

            //BOMBS
            bombCooldownCount = bombCooldownCount + Time.deltaTime;

            if (Input.GetButtonDown("Fire4") && bombCooldownCount >= bombCooldown)
            {
                Instantiate(bomb, tailPosition.position, tailPosition.rotation);
                bombCooldownCount = 0f;
            }

            //STINK WALL
            wallCooldownCount = wallCooldownCount + Time.deltaTime;
            if (Input.GetButtonDown("Fire5") && wallCooldownCount >= wallCooldown)
            {
                StartCoroutine(Wall());
                wallCooldownCount = 0f;
            }
        
            //THE WORK
            //Code that takes from above and does the work
            transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, rotateAngle, 0)); 
            movement = transform.TransformDirection(movement);
            controller.Move(movement * Time.deltaTime);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if ((other.tag == "Enemy" || other.tag == "Bomb") && isKnockbacked == false)
        {
            isKnockbacked = true;
            canControl = false;
            pushDirection = new Vector3(0,0,0);
            pushDirection = other.transform.position - transform.position;
            pushDirection =- pushDirection.normalized;
            pushDirection.y = 0;
            float i = 0;  
            gameObject.tag = "Untagged";
            while (i <= knockbackDuration)
            {
                yield return wffu;
                i += (1f * Time.deltaTime);
                controller.Move(pushDirection * Time.deltaTime * dashMoveSpeed);
            }
            canControl = true;
            color = Color.white;
            while (i <= immuneDuration)
            {
                yield return wffu;
                i += (1f * Time.deltaTime);
                if ((i > .5f && i < .7f) || (i > .9f && i < 1.1f) || (i > 1.3f && i < 1.5f)
                    || (i > 1.7f && i < 1.9f))
                {
                    color.a = .4f;
                }
                else color.a = 1f;
                playerColor.material.color = color;
                playerColor2.material.color = color;
                
            }
            gameObject.tag = "Player";
            isKnockbacked = false;
        }
    }

    private IEnumerator Wall()
    {
        float i = 0;
        float timer = .25f;
        backTrail.emitting = true;
        while (i <= wallLength)
        {
            yield return wffu;
            timer -= Time.deltaTime;
            i += (1f * Time.deltaTime);
             if (timer <= 0f)
                {
                    Instantiate(wall, tailPosition.position, tailPosition.rotation);
                    timer = 0.5f;
                }
        }
        backTrail.emitting = false;
    }
}