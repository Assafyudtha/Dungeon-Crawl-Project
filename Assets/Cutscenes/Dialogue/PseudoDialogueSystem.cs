using System;
using UnityEngine;

public class PseudoDialogueSystem
    {
        private Action dialogueCompleteCallback;

        // TODO singleton used for demonstration purposes only
        public static PseudoDialogueSystem Instance;

        public PseudoDialogueSystem()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("multiple dialogue systems exist. There should only be one");
            }
        }

        public static void ShowDialogue(PseudoDialogueSO dialogue, Action dialogueCompleteCallback)
        {
            Instance.dialogueCompleteCallback = dialogueCompleteCallback;

            // TODO play dialogue and listen for player input
            Debug.LogError("dialogue not implemented");
        }

        private void OnPlayerActionedDialogue()
        {
            dialogueCompleteCallback.Invoke();
        }
    }
