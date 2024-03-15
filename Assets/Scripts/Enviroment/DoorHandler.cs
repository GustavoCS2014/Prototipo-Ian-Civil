using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DoorHandler : MonoBehaviour
{
    [Space(10)]
    public DoorSettings TargetDoor;
    [Space(10)]
    [Header("Door Settings Creator")]
    [Space(10)]
    public string Name;
    public SceneField Scene;

    public void EnterDoor(){
        SceneTransitioner.Instance.TransitionToScene(TargetDoor);
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
                DoorSettings.CreateDoorSettings(door.Scene, door.transform.position, door.Name);
            }
        }

}
#endif

