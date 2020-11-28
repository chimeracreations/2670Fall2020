using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Image dialogue;
    public Vector3 offset;

    // Update is called once per frame
    private void OnTriggerEnter()
    {
        Vector3 dialoguePos = Camera.main.WorldToScreenPoint(this.transform.position + offset);
        dialogue.transform.position = dialoguePos;
    }
}
