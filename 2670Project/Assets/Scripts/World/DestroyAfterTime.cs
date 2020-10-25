using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeAmount;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();

     private void OnEnable() 
    {
        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine() 
    {
        float i = 0;
        while (i <= timeAmount)
        {
            yield return wffu;
            i += (1f * Time.deltaTime);
        }
        Destroy(gameObject);
    }
}
