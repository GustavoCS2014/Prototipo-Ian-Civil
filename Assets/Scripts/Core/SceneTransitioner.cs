using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Follower;
using Entities.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance {get; private set;}
    public static event Action<Vector3> OnTransitionEnded;
    const string FADE_IN = "Fade_In";
    const string FADE_OUT = "Fade_Out";

    private Animator _animator;
    private SceneField _scene;
    private Vector2? _SpawnPosition;
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

    private void Update(){
        if(_sceneLoading is null) return;
        if(_sceneLoading.isDone){
            if(_SpawnPosition is not null)
                OnTransitionEnded?.Invoke(_SpawnPosition.Value);
            _sceneLoading = null;
            _animator.Play(FADE_OUT);
        }
    }   
    /// <summary>
    /// Transitions to the scene provided.
    /// </summary>
    /// <param name="scene">Scene to load.</param>
    public void TransitionToScene(SceneField scene){
        _animator.Play(FADE_IN);
        _scene = scene;
        
        _SpawnPosition = null;
    }
    /// <summary>
    /// Transitions to the scene provided and sends the <see cref="OnTransitionEnded"/> event with the spawnPosition provided. 
    /// </summary>
    /// <param name="scene">Scene to load.</param>
    /// <param name="spawnPosition">the position sent to the event <see cref="OnTransitionEnded"/></param>
    public void TransitionToScene(SceneField scene, Vector2 spawnPosition){
        _animator.Play(FADE_IN);
        _scene = scene;
        _SpawnPosition = spawnPosition;
    }

    // llamado por el animator al finalizar FADE_IN
    public void ChangeScene(){
        _sceneLoading = SceneManager.LoadSceneAsync(_scene);
    }

}
