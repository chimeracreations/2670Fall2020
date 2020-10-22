using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public float health;
    public int numOfHearts;
    public GameObject[] hearts;
    private CharacterMover mover;
    private Respawn respawn;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHearts();
        mover = GetComponent<CharacterMover>();
        respawn = GetComponent<Respawn>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((other.tag == "Enemy" || other.tag == "Bomb") && mover.isKnockbacked == false)
        {
            health -= 0.5f;
            UpdateHearts();
        }
    }

    private void UpdateHearts()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        if (health <= 0)
        {
            StartCoroutine(respawn.playerRespawn());
            health = 3;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (health == i + .5f)
            {
                hearts[i].GetComponent<SpriteAtlasScript>().ChangeSprite("Heart_1");
            }

            else if (i < health)
            {
                hearts[i].GetComponent<SpriteAtlasScript>().ChangeSprite("Heart_0");
            }

            else
            {
                hearts[i].GetComponent<SpriteAtlasScript>().ChangeSprite("Heart_2");
            }

            if (i< numOfHearts)
            {
                hearts[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                hearts[i].GetComponent<Image>().enabled = false;
            }
        }
    }
}
