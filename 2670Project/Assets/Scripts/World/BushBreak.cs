using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBreak : MonoBehaviour
{
    private Animator animator;
    private Color color1 = new Color(0.5686f, 1, 0);
    private Color color2 = new Color(0.65098f, 0.698039f, 0.17647f);
    private Color lerpedColor;
    private Renderer[] colors;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public bool isMushroom;
    public GameObject heart;
    public GameObject bean;
    public Vector3 offset;
 
    private void Start() 
    {
        animator = GetComponent<Animator>();  
        colors = GetComponentsInChildren<Renderer>();
        if (isMushroom) color1 = new Color(0.772549f, 0.7137f, 0.43529f);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "TailStink")
        {
            var dropRandom = Random.value;
            if (dropRandom >= 0.9f)
            {
                Instantiate(heart, transform.position + offset, transform.rotation);
            }
            if (dropRandom <= 0.1f)
            {
                Instantiate(bean, transform.position + offset, transform.rotation);
            }
            animator.SetTrigger("bushDestroy");
            StartCoroutine(ColorChange());
        }

    }

    private IEnumerator ColorChange()
    {
        float t = 0;
        lerpedColor = color1;
        while (lerpedColor != color2)
        {
            lerpedColor = Color.LerpUnclamped(color1, color2, Mathf.Clamp(t,0,1));

            for (int i = 0; i < transform.childCount; i++)
            {
                colors[i].material.color = lerpedColor;
            }
            t += (Time.time / 300);            
            yield return wffu;
        }
    
    }
}
