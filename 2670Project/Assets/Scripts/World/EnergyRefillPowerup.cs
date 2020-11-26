using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefillPowerup : MonoBehaviour
{
    public PlayerData player;
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            player.energyExtraAmount += 30f;
            Destroy(gameObject);
        }
    }
    
}
