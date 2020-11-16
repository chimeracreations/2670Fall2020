using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedOnTriggerEnable : MonoBehaviour
{
    public float time;
    private WaitForSeconds wfs;

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if (other.tag == "TailStink")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return wfs = new WaitForSeconds(time);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }

    }
}
