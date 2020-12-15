using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnChangeOnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent respawnEvent;
    public int respawnNo;
    public IntData respawnUnlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            respawnEvent?.Invoke();
            if (respawnUnlock.value <= respawnNo)
            {
                respawnUnlock.value = respawnNo;
            }
        }
    }
}
