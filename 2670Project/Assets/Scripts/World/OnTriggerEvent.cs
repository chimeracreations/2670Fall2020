using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent callEvent;
   
    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.tag == "Player")
            {
                callEvent?.Invoke();
            }
        }
    }
}
