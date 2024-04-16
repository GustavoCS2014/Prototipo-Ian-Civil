using System;
using System.Collections.Generic;
using CesarJZO.DialogueSystem;
using UnityEngine;

// namespace CesarJZO.DialogueSystem
// {
    public class DialogueNpc : MonoBehaviour, IInteractable
    {
        [SerializeField] private Dialogue[] dialogues;
        [SerializeField] private Dialogue currentDialogue;

        private Queue<Dialogue> _dialogueQueue;

        private DialogueManager _dialogueManager;

        private void Awake()
        {
            _dialogueQueue = new Queue<Dialogue>(dialogues);
        }

        private void Start()
        {
            _dialogueManager = DialogueManager.Instance;

            _dialogueManager.ConditionalNodeEvaluated += OnConditionalNodeEvaluated;
            _dialogueManager.ResponseSelected += OnResponseSelected;
            _dialogueManager.ConversationUpdated += OnEndNode;

            try
            {
                currentDialogue = _dialogueQueue.Dequeue();
            }
            catch (InvalidOperationException)
            {
                Debug.LogWarning($"{name} NPC has no dialogues.", this);
            }
        }
        private void OnDestroy() {
            
            _dialogueManager.ConversationUpdated -= OnEndNode;
        }

        private void OnConditionalNodeEvaluated(bool hadItem)
        {
            var onItemMatch = GetComponent<DialogueOnItemMatch>();

            if (onItemMatch)
                onItemMatch.OnConditionalNodeEvaluated(hadItem);
        }

        private void OnResponseSelected(string response)
        {
            var onResponseSelected = GetComponent<DialogueOnResponseTrigger>();

            if (onResponseSelected)
                onResponseSelected.OnResponseSelected(response);
        }

        private void OnEndNode(){
            if(!_dialogueManager.CurrentNode) return;
            string trigger = _dialogueManager.CurrentNode.TriggerKey;
            DialogueListener[] listeners = GetComponents<DialogueListener>();
            foreach(DialogueListener listener in listeners){
                listener.EvaluateTrigger(trigger);
            }
        }

        public void Interact()
        {
            if (!currentDialogue) return;

            if (!_dialogueManager) return;

            _dialogueManager.StartDialogue(currentDialogue);
        }

        public Vector3 GetPosition() => transform.position;

        public void DequeueDialogue()
        {
            if (_dialogueQueue.TryDequeue(out Dialogue dialogue))
                currentDialogue = dialogue;
        }
    }
// }
