using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public bool canControl;
    public float healthValue;
    public int maxHealth; 
    public float gravityForce;
    public float jumpForce;
    public int maxJumpCount;
    public bool canJump;
    public float moveSpeed;
    public float fastMoveSpeed;
    public float dashMoveSpeed;
    public float dashCooldown;
    public float dashRest;
    public float recoveryMoveSpeed;
    public float diagonalRestraint;
    public float rotateSpeed;
    public float attackPause;
    public float tailEmit;
    public bool madeNoise;
    public float knockbackDuration;
    public float immuneDuration;
    public bool isKnockbacked;
    public float bombCooldown;
    public float wallCooldown;
    public float wallLength;
    [HideInInspector] public Vector3 movement;
    [HideInInspector] public CharacterController controller;
        



    
}

