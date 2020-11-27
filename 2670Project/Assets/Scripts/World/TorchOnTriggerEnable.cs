using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchOnTriggerEnable : MonoBehaviour
{
    public float time;
    private WaitForSeconds wfs;
    private WaitForFixedUpdate wffu;
    public PlayerData player;

    private void Start() 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if (other.tag == "TailStink" && player.canTorch == true)
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
