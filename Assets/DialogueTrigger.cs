using System;
using System.Collections;
using System.Collections.Generic;
using QuantumTek.QuantumDialogue;
using QuantumTek.QuantumDialogue.Demo;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueBox dialogueBox;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        dialogueBox.gameObject.SetActive(true);
        DialogSystem.instance.StartDialogue(dialogueBox.dialogue.Conversations[0].Name);
        DialogSystem.instance.messageText = dialogueBox.messageField;
        DialogSystem.instance.speakerName = dialogueBox.nameField;
    }
}
