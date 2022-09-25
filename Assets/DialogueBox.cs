using System;
using System.Collections;
using System.Collections.Generic;
using QuantumTek.QuantumDialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public TextMeshPro nameField;
    public TextMeshPro messageField;
    public QD_Dialogue dialogue;

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
