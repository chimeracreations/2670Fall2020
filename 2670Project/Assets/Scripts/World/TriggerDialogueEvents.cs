using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerDialogueEvents : MonoBehaviour
{
     public StringListData list;
     private WaitForSeconds wfs;
     private int i;
     private string returnValue;
     public Text obj;
     private int wait;
     private float sizeOfList;

     private void Start()
     {
          i = 0;
          sizeOfList = list.stringList.Count;
          wfs = new WaitForSeconds(sizeOfList - .5f);
     }
     
     private void OnTriggerEnter(Collider other)
     {
        if (other.tag == "TailStink")
          {    
               StopCoroutine(dialogueRun());
               StartCoroutine(dialogueRun());
          }
     }

     private IEnumerator dialogueRun()
     {
          string check;
          returnValue = list.stringList[i];
          check = returnValue;
          i = (i + 1) % list.stringList.Count;
          obj.text = returnValue;
          yield return wfs;
          if (check == returnValue)
          {
               obj.text = null;
               i = 0;
          }
     }
}