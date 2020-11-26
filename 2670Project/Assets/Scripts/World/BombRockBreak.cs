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

    private void Start() 
    {
        anim = GetComponent<Animator>(); 
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Bomb")
        {
            anim.SetTrigger("bombTrigger");
            if (Random.value >= 0.9f)
            {
                Instantiate(heart, transform.position + offset, transform.rotation);
            }
            if (Random.value <= 0.3f)
            {
                Instantiate(bean, transform.position + offset, transform.rotation);
            }
        }
    }
}
