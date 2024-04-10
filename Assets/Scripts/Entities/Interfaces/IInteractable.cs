using System;
using Entities.Player;
using UnityEngine;

interface IInteractable {
    /// <summary>
    /// Executes what happens when interacted.
    /// </summary>
    /// <param name="interactor">The object interacting.</param>
    void Interaction(PlayerController interactor);
    Vector3 InteractablePosition();

}
