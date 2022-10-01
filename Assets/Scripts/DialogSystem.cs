using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

namespace QuantumTek.QuantumDialogue.Demo
{
    public class DialogSystem : MonoBehaviour
    {
        public QD_DialogueHandler handler;
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI messageText;
        public Transform choices;
        public TextMeshPro choiceTemplate;

        private List<TextMeshPro> activeChoices = new List<TextMeshPro>();
        private List<TextMeshPro> inactiveChoices = new List<TextMeshPro>();

        private bool ended;

        public GameObject dialogBoxGO;
        public Sapotezos sapo;

        [SerializeField] private UnityEvent DialogStarted;
        [SerializeField] private UnityEvent DialogFinished;

        public void StartConversation(string conversation, QD_DialogueHandler newhandler)
        {
            handler = newhandler;
            handler.SetConversation(conversation);
            SetText();
            GameManager.Instance.BlockPlayerInput();
        }


        //my code above

        private void Update()
        {
            // Don't do anything if the conversation is over
            if (ended)
            {
                return;   
            }

            // Check if the space key is pressed and the current message is not a choice
            if (handler.currentMessageInfo.Type == QD_NodeType.Message && (Input.GetKeyUp(KeyCode.Space)||Input.GetKeyUp(KeyCode.C)))
                Next();
        }

        private void ClearChoices()
        {
            for (int i = activeChoices.Count - 1; i >= 0; --i)
            {
                // Use object pooling with the choices to prevent unecessary garbage collection
                activeChoices[i].gameObject.SetActive(false);
                activeChoices[i].text = "";
                inactiveChoices.Add(activeChoices[i]);
                activeChoices.RemoveAt(i);
            }
        }

        private void GenerateChoices()
        {
            // Exit if not a choice
            if (handler.currentMessageInfo.Type != QD_NodeType.Choice)
                return;
            // Clear the old choices
            ClearChoices();
            // Generate new choices
            QD_Choice choice = handler.GetChoice();
            int added = 0;
            // Use inactive choices instead of making new ones, if possible
            while (inactiveChoices.Count > 0 && added < choice.Choices.Count)
            {
                int i = inactiveChoices.Count - 1;
                TextMeshPro cText = inactiveChoices[i];
                cText.text = choice.Choices[added];
                QD_ChoiceButton button = cText.GetComponent<QD_ChoiceButton>();
                button.number = added;
                cText.gameObject.SetActive(true);
                activeChoices.Add(cText);
                inactiveChoices.RemoveAt(i);
                added++;
            }
            // Make new choices if any left to make
            while (added < choice.Choices.Count)
            {
                TextMeshPro newChoice = Instantiate(choiceTemplate, choices);
                newChoice.text = choice.Choices[added];
                QD_ChoiceButton button = newChoice.GetComponent<QD_ChoiceButton>();
                button.number = added;
                newChoice.gameObject.SetActive(true);
                activeChoices.Add(newChoice);
                added++;
            }
        }

        private void SetText()
        {
            // Clear everything
            speakerName.text = "";
            messageText.gameObject.SetActive(false);
            messageText.text = "";
            ClearChoices();

            // If at the end, don't do anything
            if (ended)
                return;

            // Generate choices if a choice, otherwise display the message
            if (handler.currentMessageInfo.Type == QD_NodeType.Message)
            {
                QD_Message message = handler.GetMessage();
                speakerName.text = message.SpeakerName;
                messageText.text = message.MessageText;
                messageText.gameObject.SetActive(true);

            }
            else if (handler.currentMessageInfo.Type == QD_NodeType.Choice)
            {
                speakerName.text = "Player";
                GenerateChoices();
            }
        }

        public void Next(int choice = -1)
        {
            if (ended)
                return;
            

            // Go to the next message
            handler.NextMessage(choice);
            // Set the new text
            SetText();
            // End if there is no next message
            if (handler.currentMessageInfo.ID < 0)
            {
                ended = true;
                dialogBoxGO.SetActive(false);
                sapo.FadeOut();
                DialogFinished?.Invoke();
                GameManager.Instance.UnblockPlayerInput();
            }
        }

        public void Choose(int choice)
        {
            if (ended)
                return;

            Next(choice);
        }
    }
}