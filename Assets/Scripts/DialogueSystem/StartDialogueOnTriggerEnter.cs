using System;
using CesarJZO.DialogueSystem;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(DialogueNpc))]
public class StartDialogueOnTriggerEnter : MonoBehaviour
{
    private DialogueNpc _npc;

    private void Awake() {
        _npc = GetComponent<DialogueNpc>();
    }
    public void DisableTrigger()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.TryGetComponent(out PlayerInteractionHandler player)) return;

        _npc.Interact();
    }

    public void Reset() {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
