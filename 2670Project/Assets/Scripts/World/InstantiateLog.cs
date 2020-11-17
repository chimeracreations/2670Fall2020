using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLog : MonoBehaviour
{
    public GameObject log;
    public Transform instancer;
    public float[] time;
    private WaitForSeconds wfs;


    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(LogSpawn());
    }

    private IEnumerator LogSpawn()
    {
        while (instancer)
        {
            for (int i = 0; i < time.Length; i++)
            {
                yield return wfs = new WaitForSeconds(time[i]);
                Instantiate(log, instancer.position, instancer.rotation);
            }
        }
    }

}
