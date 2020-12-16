using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts;
    public Text lives;
    public PlayerData player;
    private Respawn respawn;
    private WaitForSeconds wfs = new WaitForSeconds(1.5f);

    // Start is called before the first frame update
    void Start()
    {
        UpdateHearts();
        respawn = GetComponent<Respawn>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((other.tag == "Enemy" || other.tag == "EnemyAttack") && player.isKnockbacked == false)
        {
            player.healthValue -= other.gameObject.GetComponent<EnemyStats>().damageAmount;
            if( other.gameObject.GetComponent<EnemyStats>().canConfuse == true)
            {
                StartCoroutine(confused());
            }
            UpdateHearts();
        }
        if (other.tag == "Bomb" && player.isKnockbacked == false)
        {
            player.healthValue -= 0.5f;
            UpdateHearts();
        }
    }

    public void UpdateHearts()
    {
        if (player.healthValue > player.maxHealth)
        {
            player.healthValue = player.maxHealth;
        }

        if (player.healthValue <= 0)
        {
            StartCoroutine(respawn.playerRespawn());
            player.healthValue = 3;
            player.lives -= 1;
            lives.text = player.lives.ToString();
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (player.healthValue <= i + .5f && player.healthValue > i)
            {
                hearts[i].GetComponent<SpriteAtlasScript>().ChangeSprite("Heart_1");
            }

            else if (i < player.healthValue)
            {
                hearts[i].GetComponent<SpriteAtlasScript>().ChangeSprite("Heart_0");
            }

            else
            {
                hearts[i].GetComponent<SpriteAtlasScript>().ChangeSprite("Heart_2");
            }

            if (i< player.maxHealth)
            {
                hearts[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                hearts[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    public IEnumerator confused()
    {
        player.reversedMove = true;
        yield return wfs;
        player.reversedMove = false;
    }

    public void DamageHealth(float damageAmount)
    {
        player.healthValue -= damageAmount;
        UpdateHearts();
    }
}
