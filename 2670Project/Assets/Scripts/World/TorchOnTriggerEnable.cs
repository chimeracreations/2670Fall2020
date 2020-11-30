using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TorchOnTriggerEnable : MonoBehaviour
{
    public float time;
    private WaitForSeconds wfs;
    private WaitForFixedUpdate wffu;
    public PlayerData player;
    public IntData torchCount;
    public bool activateEvent;
    public bool countSequence;
    public int orderNumber;
    public int eventCount;
    [SerializeField] private UnityEvent torchEvent, torchEventTwo;
    private bool count = false;




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
            if (countSequence == true)
            {
                if (torchCount.value >= orderNumber)
                {
                    if (orderNumber == torchCount.value) torchCount.value++;

                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }

                    if (activateEvent == true && count == false && eventCount == torchCount.value)
                    {
                        torchEvent?.Invoke();
                        count = true;
                    }
                }
                else
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    StartCoroutine(LightsOut());
                }
            }
            
            if (activateEvent == true && count == false && !countSequence)
            {
                torchEvent?.Invoke();
                count = true;
                if (torchCount.value == eventCount)
                {
                    torchEventTwo?.Invoke();
                }
            }

            if (countSequence == false)
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
    public IEnumerator LightsOut()
    {
        
        for (int i = 0; i < transform.childCount; i++)
            {
                yield return wfs = new WaitForSeconds(1);
                transform.GetChild(i).gameObject.SetActive(false);
            }
    }
}
