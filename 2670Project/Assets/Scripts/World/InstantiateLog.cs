using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLog : MonoBehaviour
{
    public GameObject log;
    public Transform instancer;
    public float[] time;
    private int count = 0;
    private float countToSeconds;
    private int i = 0;

    private void FixedUpdate()
    {
        count++;
        countToSeconds = (count / 50f);
        if (i < time.Length)
        {
            if (time[i] == countToSeconds)
            {
                var thisLog = Instantiate(log, instancer.position, instancer.rotation);
                thisLog.transform.parent = gameObject.transform;
                count = 0;
                i++;
            }
        }
    }

    // private IEnumerator LogSpawn()
    // {
    //     int i = 0;
    //     while (i < time.Length)
    //     {
    //         for (i = 0; i < time.Length; i++)
    //         {
    //             yield return wfs = new WaitForSeconds(time[i]);
    //             var thisLog = Instantiate(log, instancer.position, instancer.rotation);
    //             thisLog.transform.parent = gameObject.transform;
    //         }
    //     }
    // }

}
