using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Door Settings", menuName = "Door Settings")]
public class DoorSettings : ScriptableObject {
    
    [Tooltip("The name of the door.")]
    [SerializeField] private string doorName;
    public string DoorName => doorName;

    [Tooltip("The Scene where the door resides.")]
    [SerializeField] private SceneField doorScene;
    public SceneField DoorScene => doorScene;

    [Tooltip("The location of the door.")]
    [SerializeField] private Vector3 doorPosition;
    public Vector3 DoorPosition => doorPosition;

    private void Init(SceneField scene, Vector3 position, string name){
        doorScene = scene;
        doorPosition = position;
        doorName = name;
    }

    public static DoorSettings CreateDoorSettings(SceneField scene, Vector3 position, string name){
        DoorSettings settings = ScriptableObject.CreateInstance<DoorSettings>();
        settings.Init(scene, position, name);
        
        string savePath = $"Assets/Game/Settings/DoorSettings/{name}_DoorSettings.asset";
        AssetDatabase.CreateAsset(settings, savePath);
        AssetDatabase.SaveAssetIfDirty(settings);

        return settings;
    }

}
