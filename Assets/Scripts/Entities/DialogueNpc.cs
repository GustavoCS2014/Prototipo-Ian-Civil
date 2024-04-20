using System;
using System.Collections.Generic;
using CesarJZO.DialogueSystem;
using UnityEngine;
using Random = System.Random;

// namespace CesarJZO.DialogueSystem
// {
public class DialogueNpc : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform interactionHintVisual;
        [SerializeField] private bool randomizeNextDialogue;
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
            if(randomizeNextDialogue)
                ShufleDialogueQueue();
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

        public void ShufleDialogueQueue(){
            Dialogue[] shuffledArray = dialogues;

            Random random = new Random();
            int arrayLenght = shuffledArray.Length;
            while(arrayLenght > 1){
                int randomIndex = random.Next(arrayLenght--);
                Dialogue tempDialogue = shuffledArray[arrayLenght];
                shuffledArray[arrayLenght] = shuffledArray[randomIndex];
                shuffledArray[randomIndex] = tempDialogue;
            } 

            dialogues = shuffledArray;
            _dialogueQueue = new Queue<Dialogue>(dialogues);
            currentDialogue = _dialogueQueue.Peek();
        }

    public void ShowInteractionHint(bool setActive){
        if(interactionHintVisual is null) return;
        interactionHintVisual.gameObject.SetActive(setActive);
    }
}
// }
