using System.Collections.Generic;
using Entities.Follower;
using Entities.Player;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using CesarJZO.DialogueSystem;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float interactionRadius;

    private IInteractable _lastInteractable;
    private PlayerController _playerController;

    private void Start() {
        _playerController = PlayerController.Instance;
        GameplayInput.OnInteract += OnInteractInput;
    }

    private void LateUpdate(){
        ShowInteractableHint();
    }

    #region  HANDLING DOORS
    private void OnTriggerStay2D(Collider2D other) {
        if(other.TryGetComponent<DoorHandler>(out DoorHandler door)){
            if(GameplayInput.MoveDirection.y < .25f) return;
            door.EnterDoor();
        }
    }
    #endregion

    private void OnDrawGizmos() {
        // Debuging.
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
        if(GetClosestInteractable() == null) return;
        
        IInteractable interactable = GetClosestInteractable();
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(interactable.GetPosition() + Vector3.up * 2.5f, .25f);
        
    }

    private void ShowInteractableHint(){
        if(_lastInteractable is not null) _lastInteractable.ShowInteractionHint(false);
        _lastInteractable = GetClosestInteractable();
        if(_lastInteractable is not null) _lastInteractable.ShowInteractionHint(true);
    }

    private void OnInteractInput(InputAction.CallbackContext context){
        if(context.canceled) return;
        if(GetClosestInteractable() == null) return;

        IInteractable interactable = GetClosestInteractable();
        if(interactable is IFollowerInteractable){
            HandleFollowerInteractables(interactable as IFollowerInteractable);
            return;
        }

        interactable.Interact();
    }


    /// <summary>
    /// If the follower was following, remove from following.
    /// </summary>
    /// <param name="follower">The follower in question.</param>
    private void HandleFollowerInteractables(IFollowerInteractable follower){
        follower.FollowerInteraction(GetComponent<PlayerController>(), out FollowerController followerController);
    }

    private IInteractable GetClosestInteractable(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactionLayer);
        IInteractable closestInteractable = null;
        float lastDistance = interactionRadius + 5f;

        foreach(Collider2D collider in colliders){
            //! NOTE the TryGetComponent does not work with files from other namespaces, even if you add it to using. it just says it's false.
            if(!collider.gameObject.TryGetComponent(out IInteractable currentInteractable)) break;
            float distance = Vector2.Distance(collider.transform.position, transform.position);
            if(distance > lastDistance) break;
            lastDistance = distance;
            closestInteractable = currentInteractable;
        }

        return closestInteractable;
    }
}