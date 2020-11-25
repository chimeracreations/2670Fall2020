using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnergyBehaviour : MonoBehaviour
{
    public Slider energySlider;
    public Slider energyRegenSlider;
    public PlayerData player;
    private float difference;
    private float surplus;

    // Start is called before the first frame update
    void Start()
    {
        player.energyRefillAmount = 30f;
        player.energyRefillMax = 30f;
        player.energyExtraAmount = 70f;
        player.energyExtraMax = 70f;
        player.energyDamage = 0f;
        player.energyTotal = 100f;
        energySlider.maxValue = player.energyExtraMax;
        energyRegenSlider.maxValue = player.energyRefillMax;
    }

    private void Update() 
    {
        if (player.energyExtraAmount > player.energyExtraMax)
        {
            surplus = player.energyExtraAmount - player.energyExtraMax;
            player.energyDamage -= surplus;
        }
        
        if (player.energyRefillAmount > player.energyRefillMax)
        {
            player.energyRefillAmount = player.energyRefillMax;
        }

        if (player.energyExtraAmount > player.energyExtraMax)
        {
            player.energyExtraAmount = player.energyExtraMax;
        }

        player.energyRefillAmount = Mathf.Clamp((player.energyRefillMax - player.energyDamage),0f,30f);
        if (player.energyDamage > 30)
        {
            difference = player.energyDamage - 30;
        }
        player.energyDamage = Mathf.Clamp((player.energyDamage - (player.energyRefillRate * Time.deltaTime)),0f,30f);

        if (player.energyRefillAmount <= 0 && player.energyExtraAmount > 0)
        {
            player.energyExtraAmount = Mathf.Clamp((player.energyExtraAmount - difference / 2),0f,70f);
        }

        player.energyTotal = player.energyExtraAmount + player.energyRefillAmount;



        energySlider.value = player.energyExtraAmount;
        energyRegenSlider.value = player.energyRefillAmount;
    }
  
}
