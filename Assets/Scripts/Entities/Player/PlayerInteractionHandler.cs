using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(other.TryGetComponent<DoorHandler>(out DoorHandler door)){
            if(GameplayInput.MoveDirection.y < .25f) return;
            door.EnterDoor();
        }
    }
}
