using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Entities.Follower;
using Entities.Player;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionHandler : MonoBehaviour
{
    [field: SerializeField] public List<FollowerController> ActiveFollowers {get; private set;} 
    [SerializeField] private float interactionRadius;

    private void Start() {
        GameplayInput.OnInteract += OnInteractInput;
    }

    private void OnInteractInput(InputAction.CallbackContext context){
        if(context.canceled) return;
        if(GetClosestInteractable() == null) return;

        IInteractable interactable = GetClosestInteractable();
        if(interactable is IFollowerInteractable){
            handleFollowerInteractables(interactable as IFollowerInteractable);
            return;
        }

        interactable.Interaction(GetComponent<PlayerController>());
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
        if(GetClosestInteractable() == null) return;
        
        IInteractable interactable = GetClosestInteractable();
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(interactable.InteractablePosition() + Vector3.up * 2.5f, .25f);
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.TryGetComponent<DoorHandler>(out DoorHandler door)){
            if(GameplayInput.MoveDirection.y < .25f) return;
            door.EnterDoor();
        }
    }

    private void handleFollowerInteractables(IFollowerInteractable follower){
        follower.FollowerInteraction(GetComponent<PlayerController>(), out FollowerController followerController);
        if(ActiveFollowers.Contains(followerController)){
            ActiveFollowers.Remove(followerController);
            return;
        };
        ActiveFollowers.Add(followerController);
    }

    private IInteractable GetClosestInteractable(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius);
        IInteractable closestInteractable = null;
        float lastDistance = interactionRadius + 5f;

        foreach(Collider2D collider in colliders){
            if(!collider.TryGetComponent<IInteractable>(out IInteractable currentInteractable)) break;
            float distance = Vector3.Distance(collider.transform.position, transform.position);
            if(distance > lastDistance) break;
            lastDistance = distance;
            closestInteractable = currentInteractable;
        }

        return closestInteractable;
    }
}
