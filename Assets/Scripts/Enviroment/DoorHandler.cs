using System.Collections;
using System.Collections.Generic;
using Core;
using Entities.Player;
using Management;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DoorHandler : MonoBehaviour
{
    [SerializeField] private SceneField EnterScene;

    public void EnterDoor(){
        SceneTransitioner.Instance.TransitionToScene(EnterScene);
    }



}
