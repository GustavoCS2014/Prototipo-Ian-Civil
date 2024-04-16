using UnityEngine;
using UnityEngine.Events;

public sealed class DialogueListener : MonoBehaviour {
    
    [SerializeField] private string triggerToEvaluate;
    [SerializeField] private UnityEvent onTriggerMatch;

    public void EvaluateTrigger(string triggerKey){
        if(triggerKey != triggerToEvaluate) return;
        onTriggerMatch?.Invoke();
    }

}