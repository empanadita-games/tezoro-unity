using System;
using System.Collections;
using System.Collections.Generic;
using QuantumTek.QuantumDialogue;
using QuantumTek.QuantumDialogue.Demo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public TextMeshPro nameField;
    public TextMeshPro messageField;
    public QD_DialogueHandler handler;
    public DialogSystem dialogSystem;
    public int conversationNr;
    
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
