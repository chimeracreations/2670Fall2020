﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverFlow : MonoBehaviour
{
    private GameObject character;
    private CharacterController cc;
    private PlayerHealth health;
    public PlayerData player;
    public float riverSpeed = 10f;
    public float damageSpeed = 0.5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindWithTag("Player");
        //cc = character.GetComponent<CharacterController>();
        health = character.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            player.offset.z = (-riverSpeed);
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player")
        {
            player.offset.z = (-riverSpeed);
            player.healthValue -= damageSpeed * Time.deltaTime;
            health.UpdateHearts();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
            player.offset.z = 0f;

        }
    }
}
