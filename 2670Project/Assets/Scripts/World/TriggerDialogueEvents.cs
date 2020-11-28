using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerDialogueEvents : MonoBehaviour
{
     public StringListData list;
     private WaitForSeconds wfs;
     private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
     private int i;
     private string returnValue;
     public Text obj;
     private int wait;
     private float sizeOfList;
     public Image textBox;
     public Vector3 offset;
     private Vector3 dialoguePos;
     private bool isLocating = false;
     [SerializeField] private UnityEvent Ability;

     private void Start()
     {
          textBox.enabled = false;
          i = 0;
          sizeOfList = list.stringList.Count;
          wfs = new WaitForSeconds(sizeOfList - .5f);
     }
     
     private void OnTriggerEnter(Collider other)
     {
        if (other.tag == "TailStink")
          {    
               Ability.Invoke();
               StopCoroutine(dialogueRun());
               StartCoroutine(dialogueRun());
               StartCoroutine(dialogueLocation());

          }
     }

     public void CallDialogue()
     {
          StopCoroutine(dialogueRun());
          StartCoroutine(dialogueRun());
          StartCoroutine(dialogueLocation());
     }

     private IEnumerator dialogueRun()
     {
          isLocating = true;
          textBox.enabled = true;
          string check;
          returnValue = list.stringList[i];
          check = returnValue;
          i = (i + 1) % list.stringList.Count;
          obj.text = returnValue;
          yield return wfs;
          if (check == returnValue)
          {
               obj.text = null;
               textBox.enabled = false;
               i = 0;
               isLocating = false;
          }
     }

     private IEnumerator dialogueLocation()
     {
          while (isLocating == true)
          {
               dialoguePos = Camera.main.WorldToScreenPoint(this.transform.position + offset);
               textBox.transform.position = dialoguePos;
               yield return wffu;
          }
     }
}