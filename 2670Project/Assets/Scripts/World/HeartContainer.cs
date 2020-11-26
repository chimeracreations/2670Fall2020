using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : MonoBehaviour
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
            player.maxHealth += 1;
            player.healthValue = player.maxHealth;
            health.UpdateHearts();
            Destroy(gameObject);
        }
    }
    
}
