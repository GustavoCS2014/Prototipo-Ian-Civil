using System;
using Entities.Player;
using UnityEngine;

interface IInteractable {
    /// <summary>
    /// Executes what happens when interacted.
    /// </summary>
    void Interact();
    Vector3 GetPosition();

}
