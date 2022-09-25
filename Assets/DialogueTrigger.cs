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
        DialogSystem.instance.StartConversation(dialogueBox.handler.dialogue.Conversations[dialogueBox.conversationNr].Name, dialogueBox.handler);
        DialogSystem.instance.messageText = dialogueBox.messageField;
        DialogSystem.instance.speakerName = dialogueBox.nameField;
    }
}
