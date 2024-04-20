using System;
using System.Collections.Generic;
using CesarJZO.InventorySystem;
using Entities.Follower;
using Entities.Player;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DoorHandler : MonoBehaviour
{
    [SerializeField] private DoorSettings targetDoor;
    [Tooltip("Using the inventory to check the player keys. (and see if he has followers)")]
    [SerializeField] private Inventory playerInventory;
    [Tooltip("The list of keys checking if the NPC is following Ian")]
    [SerializeField] private Item[] followerKeys;
    [Tooltip("The list of the NPCs that can follow Ian (Add in the same order as the keys)\n WARNING! ONLY INSTANCES IN SCENE, NOT PREFABS.")]
    [SerializeField] private GameObject[] followerInstances;
    [Space(10)]
    [Header("Door Settings Creator")]
    [Space(10)]
    public string DoorName;
    public SceneField DoorScene;

    private void Start() {
        SceneTransitioner.OnTransitionEnded += OnDoorTransitionEndedEvent;
    }
    private void OnDestroy() {
        SceneTransitioner.OnTransitionEnded -= OnDoorTransitionEndedEvent;
    }

    private void OnDoorTransitionEndedEvent(Vector3 exitPosition)
    {
        ExitDoor(exitPosition);
    }

    public void EnterDoor(){
        SceneTransitioner.Instance.TransitionToScene(targetDoor.DoorScene, targetDoor.DoorPosition);
    }

    public void ExitDoor(Vector3 exitPosition){ 
        if(exitPosition != transform.position) return;
        PlayerController.Instance.SetPosition(exitPosition);
        // This whole implementation using the inventory to check the followers is shit, please FTLOG change it on production.
        // And also, move this to somewhere else, this shouldn't be here.
        // Holy fk, for some reason "PrefabUtility.FindAllInstancesOfPrefab" didn't find any instance of the prefabs
        // So i had to manually set the NPC scene instances references
        // pleeeease try to find a better way to do this.
        for(int i = 0; i < followerKeys.Length; i++){
            if(!playerInventory.HasItem(followerKeys[i])) return;

            GameObject follower = followerInstances[i];

            if(follower.TryGetComponent(out FollowerController controller)){
                controller.SetPosition(exitPosition);
                controller.SetTarget(PlayerController.Instance.transform);            
            }
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(DoorHandler)), CanEditMultipleObjects]
public class DoorHandlerEditor : Editor {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DoorHandler door = (DoorHandler)target;

            GUILayout.Space(20);
            if(GUILayout.Button("Generate Door Settings", GUILayout.Height(40))){
                DoorSettings.CreateDoorSettings(door.DoorScene, door.transform.position, door.DoorName);
            }
        }

}
#endif

