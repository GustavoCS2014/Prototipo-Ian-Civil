using System;
using Entities.Player;
using UnityEngine;

interface IInteractable {
    /// <summary>
    /// Executes what happens when interacted.
    /// </summary>
    void Interact();
    void ShowInteractionHint(bool SetActive);
    Vector3 GetPosition();

}
