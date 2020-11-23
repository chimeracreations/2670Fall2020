using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BombRockBreak : MonoBehaviour
{
    private Animator anim;

    private void Start() 
    {
        anim = GetComponent<Animator>(); 
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Bomb")
        {
            anim.SetTrigger("bombTrigger");
        }
    }
}
