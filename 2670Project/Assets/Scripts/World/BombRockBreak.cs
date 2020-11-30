using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BombRockBreak : MonoBehaviour
{
    private Animator anim;
    public GameObject heart;
    public GameObject bean;
    public Vector3 offset;
    private bool hasTriggered = false;

    private void Start() 
    {
        anim = GetComponent<Animator>(); 
    }
    public void CallBombBreak()
    {
        anim.SetTrigger("bombTrigger");
    }

    public void DestroyBombRock()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Bomb" && hasTriggered == false)
        {
            anim.SetTrigger("bombTrigger");
            var dropRandom = Random.value;
            if (dropRandom >= 0.9f)
            {
                Instantiate(heart, transform.position + offset, transform.rotation);
            }
            if (dropRandom <= 0.3f)
            {
                Instantiate(bean, transform.position + offset, transform.rotation);
            }
            hasTriggered = true;
        }
    }
}
