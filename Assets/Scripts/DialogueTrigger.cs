using System;
using System.Collections;
using System.Collections.Generic;
using QuantumTek.QuantumDialogue;
using QuantumTek.QuantumDialogue.Demo;
using UnityEngine;
using UnityStandardAssets._2D;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueBox dialogueBox;
    private bool blockPlayerMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (!gameObject.activeSelf) return;
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.dialogSystem.StartConversation(dialogueBox.handler.dialogue.Conversations[dialogueBox.conversationNr].Name, dialogueBox.handler);
        dialogueBox.dialogSystem.messageText = dialogueBox.messageField;
        dialogueBox.dialogSystem.speakerName = dialogueBox.nameField;
        gameObject.SetActive(false);
        if (blockPlayerMovement)
        {
            var player = other.GetComponent<PlayerInput>();
            player.BlockInput();
        }
    }

    public void IndependentShowDialogue()
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.dialogSystem.StartConversation(dialogueBox.handler.dialogue.Conversations[dialogueBox.conversationNr].Name, dialogueBox.handler);
        dialogueBox.dialogSystem.messageText = dialogueBox.messageField;
        dialogueBox.dialogSystem.speakerName = dialogueBox.nameField;
        gameObject.SetActive(false);
    }
}
