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

    // Start is called before the first frame update
    void Start()
    {
        UpdateHearts();
        mover = GetComponent<CharacterMover>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Enemy" && mover.isKnockbacked == false)
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
            health = 0;
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
