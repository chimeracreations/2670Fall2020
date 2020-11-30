using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppear : MonoBehaviour
{
    private Color alphaColor;
    public float timeToAppear = 1.0f;
    private MeshRenderer[] children;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            alphaColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            alphaColor.a = 1;
            other.gameObject.GetComponentInParent<EnemyKnockbackAndHealth>().revealed = true;
            children = other.gameObject.GetComponentsInChildren<MeshRenderer>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(other.GetComponent<MeshRenderer>().material.color, alphaColor, timeToAppear * Time.deltaTime);
            for (int x = 0; x <= (transform.childCount + 1); x++)
            {
                children[x].material.color = Color.Lerp(other.GetComponent<MeshRenderer>().material.color, alphaColor, timeToAppear * Time.deltaTime);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.gameObject.tag == "Enemy")
        {
             other.gameObject.GetComponentInParent<EnemyKnockbackAndHealth>().revealed = false;   
        }
    }

}
