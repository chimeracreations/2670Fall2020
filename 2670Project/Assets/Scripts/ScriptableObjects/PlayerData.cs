using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public bool canControl;
    public bool canTorch;
    public bool canPlatform;
    public bool canJump;
    public bool canBomb;
    public bool canWall;
    public bool reversedMove;
    public float healthValue;
    public int maxHealth; 
    public float energyRefillAmount;
    public float energyRefillMax;
    public float energyRefillRate;
    public float energyExtraAmount;
    public float energyExtraMax;
    public float energyTotal;
    public float energyDamage;
    public float gravityForce;
    public float jumpForce;
    public int maxJumpCount;
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
    public bool onPlatform;
    [HideInInspector] public Vector3 movement;
    [HideInInspector] public CharacterController controller;
        

    public void SetJump(bool set)
    {
        canJump = set;
    }

    public void SetTorch(bool set)
    {
        canTorch = set;
    }

    public void SetPlatform(bool set)
    {
        canPlatform = set;
    }

    public void SetBomb(bool set)
    {
        canBomb = set;
    }

    public void SetWall(bool set)
    {
        canWall = set;
    }

    public void SetJumpCount(int set)
    {
        maxJumpCount = set;
    }
    
}

