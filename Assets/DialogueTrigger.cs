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
        dialogueBox.dialogSystem.StartConversation(dialogueBox.handler.dialogue.Conversations[dialogueBox.conversationNr].Name, dialogueBox.handler);
        dialogueBox.dialogSystem.messageText = dialogueBox.messageField;
        dialogueBox.dialogSystem.speakerName = dialogueBox.nameField;
        gameObject.SetActive(false);
    }
}
