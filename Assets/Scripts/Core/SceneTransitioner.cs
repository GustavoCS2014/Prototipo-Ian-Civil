using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance {get; private set;}

    [SerializeField] private Animator animator;
    const string FADE_IN = "Fade_In";
    const string FADE_OUT = "Fade_Out";
    private AsyncOperation Transition;
    private SceneField _scene;
    public bool SceneChanged;

    private void Awake() {
        if(Instance){
            Destroy(this);
        }else{
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void TransitionToScene(SceneField scene){
        animator.Play(FADE_IN);
        _scene = scene;
    }

    public void ChangeScene(){
        SceneManager.LoadScene(_scene);
        SceneChanged = true;
    }

    private void Update(){
        if(SceneChanged){
            animator.Play(FADE_OUT);
            SceneChanged = false;
        }
    }

}
