﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Text dialogue;

    // Update is called once per frame
    void Update()
    {
        Vector3 dialoguePos = Camera.main.WorldToScreenPoint(this.transform.position);
        dialogue.transform.position = dialoguePos;
    }
}
