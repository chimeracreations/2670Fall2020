using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniBossMouth : MonoBehaviour
{
    [SerializeField] private UnityEvent callEvent;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BombPoint")
        {
            callEvent?.Invoke();
        }
    }
}
