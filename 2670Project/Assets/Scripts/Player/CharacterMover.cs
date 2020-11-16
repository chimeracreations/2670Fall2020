using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    public PlayerData player;
    private int jumpCount;
    private float rotateAngle;
    private float deltaAngle;
    private float dashCount = 0f;
    private float dashRestCount = 0f;
    private bool canDash = true;
    private TrailRenderer dashTrail;
    private Animator animator;
    private float attackPauseCount;
    private GameObject tailStink;
    private TrailRenderer tailTrail;
    private float tailEmitCount;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    private Vector3 pushDirection;
    private GameObject playerModel;
    private GameObject tail;
    private Transform tailPosition;
    private TrailRenderer backTrail;
    private Collider tailCol;
    private Renderer playerColor;
    private Renderer playerColor2;
    private Color color;
    public GameObject bomb;
    private float bombCooldownCount = 5f;
    private float wallCooldownCount = 8f;
    public GameObject wall;
    
    
    void Start()
    {
        player.controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        tail = GameObject.FindGameObjectWithTag("Tail");
        tailPosition = tail.transform;
        backTrail = tail.GetComponent<TrailRenderer>();
        backTrail.emitting = false;
        playerColor = playerModel.GetComponent<Renderer>();
        playerColor2 = tail.GetComponentInParent<Renderer>();
        attackPauseCount = player.attackPause;
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
        player.madeNoise = false;
        if (player.canControl == true)
        {
            //Constant increasing downward movement for gravity  
            player.movement.y -= player.gravityForce * Time.deltaTime;
            
            //JUMP
            //Keep downward movement from going out of control by reseting gravity but with extra umph to keep grounded. Reset jump count when hits the ground 
            if (player.controller.isGrounded)
            {
                player.movement.y = -3;
                jumpCount = 0;
            }

            //incase you fall from a cliff without jumping first
            else if (player.controller.isGrounded == false && jumpCount < 1)
                jumpCount++;

            //Jump up as many times as jumpCount is set to
            if (Input.GetButtonDown("Jump") && (jumpCount < player.maxJumpCount) && player.canJump)
            {
                player.movement.y = player.jumpForce;
                jumpCount++;
            }
        
            //MOVEMENT
            //If canDash is true and Fire2 is pressed, sets dashCount to a positive number to allow the next if statement to trigger
            if (Input.GetButtonDown("Fire2") && canDash) 
            {
                dashCount = dashCount + 1 * Time.deltaTime;
                dashTrail.emitting = true;
                player.madeNoise = true;
            }

            //Moves the character at dash speed for a set time. dashCooldown is not 1 value equals 1 second but should run off real time
            else if (dashCount > 0 && dashCount < player.dashCooldown)
            {
                player.movement.x = Input.GetAxis("Horizontal") * player.dashMoveSpeed;
                player.movement.z = Input.GetAxis("Vertical") * player.dashMoveSpeed;
                dashCount = dashCount + 1 * Time.deltaTime;
                player.madeNoise = true;
            }

            //Resets dashCount and canDash once dashcount reaches the dashCooldown
            else if (dashCount >= player.dashCooldown)
            {
                dashCount = 0;
                canDash = false;  
                dashTrail.emitting = false;       
            }

            else if (canDash == false)
            {
                player.movement.x = Input.GetAxis("Horizontal") * player.recoveryMoveSpeed;
                player.movement.z = Input.GetAxis("Vertical") * player.recoveryMoveSpeed;
                dashRestCount = dashRestCount + 1 * Time.deltaTime;
                if (dashRestCount >= player.dashRest)
                {
                    canDash = true;
                    dashRestCount = 0;
                }
            }
            //Fast movement
            else if (Input.GetButton("Fire3") && canDash)
            {
                player.movement.x = Input.GetAxis("Horizontal") * player.fastMoveSpeed;
                player.movement.z = Input.GetAxis("Vertical") * player.fastMoveSpeed;
                player.madeNoise = true;
            }
            //Regular movement
            else if (canDash)
            {
                player.movement.x = Input.GetAxis("Horizontal") * player.moveSpeed;
                player.movement.z = Input.GetAxis("Vertical") * player.moveSpeed;
            }

            //ROTATE
            //Attack animation freezes rotation until idle animation resumes
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerIdle"))
            {    
                float angleOne = rotateAngle;
                //Player rotation based off input
                if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, 45, (player.rotateSpeed * Time.deltaTime));
                    player.movement.x = player.movement.x * player.diagonalRestraint;
                    player.movement.z = player.movement.z * player.diagonalRestraint;
                }
                if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, -45, (player.rotateSpeed * Time.deltaTime));
                    player.movement.x = player.movement.x * player.diagonalRestraint;
                    player.movement.z = player.movement.z * player.diagonalRestraint;
                }
                if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, 135, (player.rotateSpeed * Time.deltaTime));
                    player.movement.x = player.movement.x * player.diagonalRestraint;
                    player.movement.z = player.movement.z * player.diagonalRestraint;
                }
                if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, -135, (player.rotateSpeed * Time.deltaTime));
                    player.movement.x = player.movement.x * player.diagonalRestraint;
                    player.movement.z = player.movement.z * player.diagonalRestraint;
                }
                if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, 90, (player.rotateSpeed * Time.deltaTime));
                }
                if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, -90, (player.rotateSpeed * Time.deltaTime));
                }
                if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, 0, (player.rotateSpeed * Time.deltaTime));
                }
                if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0)
                {
                    rotateAngle = Mathf.LerpAngle(rotateAngle, 180, (player.rotateSpeed * Time.deltaTime));
                }

                //Get the deltaAngle by subracting the before and after rotateAngle
                float angleTwo = rotateAngle;
                deltaAngle = angleOne - angleTwo;
                //Attack pause counter
                attackPauseCount = attackPauseCount + (attackPauseCount * Time.deltaTime);
            }

            //ATTACK
            //Attack animation after attack pause count 
            if (Input.GetButtonDown("Fire1") && deltaAngle <= 0 && attackPauseCount > player.attackPause)
            {
                tailCol.enabled = true;
                tailEmitCount = 0;
                animator.SetTrigger("playerAttackR");
                attackPauseCount = 1f;

            }
            else if (Input.GetButtonDown("Fire1") && deltaAngle > 0 && attackPauseCount > player.attackPause)
            {
                tailCol.enabled = true;
                tailEmitCount = 0;
                animator.SetTrigger("playerAttackL");
                attackPauseCount = 1f;

            }
            tailEmitCount = tailEmitCount + 1 * Time.deltaTime;
            if (tailEmitCount < player.tailEmit)
            {
                tailTrail.emitting = true;
                player.madeNoise = true;
            }
            else 
            {
                tailTrail.emitting = false;
                tailCol.enabled = false;
            }

            //BOMBS
            bombCooldownCount = bombCooldownCount + Time.deltaTime;

            if (Input.GetButtonDown("Fire4") && bombCooldownCount >= player.bombCooldown)
            {
                Instantiate(bomb, tailPosition.position, tailPosition.rotation);
                bombCooldownCount = 0f;
            }

            //STINK WALL
            wallCooldownCount = wallCooldownCount + Time.deltaTime;
            if (Input.GetButtonDown("Fire5") && wallCooldownCount >= player.wallCooldown)
            {
                StartCoroutine(Wall());
                wallCooldownCount = 0f;
            }
        
            //THE WORK
            //Code that takes from above and does the work
            transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, rotateAngle, 0)); 
            player.movement = transform.TransformDirection(player.movement);
            player.controller.Move(player.movement * Time.deltaTime);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if ((other.tag == "Enemy" || other.tag == "Bomb") && player.isKnockbacked == false)
        {
            player.isKnockbacked = true;
            player.canControl = false;
            pushDirection = new Vector3(0,0,0);
            pushDirection = other.transform.position - transform.position;
            pushDirection =- pushDirection.normalized;
            pushDirection.y = 0;
            float i = 0;  
            gameObject.tag = "Untagged";
            while (i <= player.knockbackDuration)
            {
                yield return wffu;
                i += (1f * Time.deltaTime);
                player.controller.Move(pushDirection * Time.deltaTime * player.dashMoveSpeed);
            }
            player.canControl = true;
            color = Color.white;
            while (i <= player.immuneDuration)
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
            player.isKnockbacked = false;
        }
    }

    private IEnumerator Wall()
    {
        float i = 0;
        float timer = .25f;
        backTrail.emitting = true;
        while (i <= player.wallLength)
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