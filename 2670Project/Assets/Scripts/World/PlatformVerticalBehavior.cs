using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVerticalBehavior : MonoBehaviour
{
  
    public GameObject ColliderNRenderer;
    private Animator animator;
    public PlayerData player;

    private void Start() 
    {
        animator = ColliderNRenderer.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "TailStink")
        {
            animator.SetTrigger("raisePlatform");
        }
    }

    private void OnEnable() 
    {
        player.onPlatform = false;
    }
}
