using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterTime : MonoBehaviour
{
    private WaitForSeconds wfs;

    public IEnumerator DoEnable(float time)
    {
        yield return wfs = new WaitForSeconds(time);
        
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void CallEnable(float time)
    {
        StartCoroutine(DoEnable(time));
    }

    public void CallDisable()
    {
        StopCoroutine(DoEnable(0));
        transform.GetChild(0).gameObject.SetActive(false);
    }

    
}
