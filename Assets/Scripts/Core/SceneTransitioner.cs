using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance {get; private set;}
    const string FADE_IN = "Fade_In";
    const string FADE_OUT = "Fade_Out";

    private Animator _animator;
    private SceneField _scene;
    private Vector3 _doorPosition;
    private AsyncOperation _sceneLoading;
    public bool SceneChanged {get; private set;}

    private void Awake() {
        if(Instance){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
        _animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void TransitionToScene(DoorSettings door){
        _animator.Play(FADE_IN);
        _scene = door.DoorScene;
        _doorPosition = door.DoorPosition;
    }

    public void ChangeScene(){
        _sceneLoading = SceneManager.LoadSceneAsync(_scene);
    }

    private void Update(){
        if(_sceneLoading is null) return;
        if(_sceneLoading.isDone){
            _animator.Play(FADE_OUT);
            PlayerController player = FindObjectOfType<PlayerController>();
            player.SetPosition(_doorPosition);
            _sceneLoading = null;
        }
    }

}
