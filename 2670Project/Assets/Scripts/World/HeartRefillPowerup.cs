using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRefillPowerup : MonoBehaviour
{
    public PlayerData player;
    private PlayerHealth health;
    private GameObject character;

    private void Start() 
    {
        character = GameObject.FindGameObjectWithTag("Player");
        health = character.GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            player.healthValue += 1f;
            health.UpdateHearts();
            Destroy(gameObject);
        }
    }
    
}