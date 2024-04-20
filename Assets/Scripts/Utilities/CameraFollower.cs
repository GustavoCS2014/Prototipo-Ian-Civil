using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private enum UpdateType{
        Update,
        FixedUpdate,
        LateUpdate
    }

    [SerializeField] private UpdateType updateType;
    private Transform target;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float speed;
    [SerializeField, Range(0.2f, 10f)] private float maxDistance;
    [SerializeField, Range(.001f, 4f)] private float minDistance;
    [SerializeField] private bool showDebug;

    private Vector3 desiredPosition;

    private void Start() {
        target = PlayerController.Instance.transform;
        desiredPosition = target.position;
        transform.position = (desiredPosition + offset);
    }

    private void Update(){
        if(updateType != UpdateType.Update) return;
        FollowTarget();
    }

    private void FixedUpdate() {
        if(updateType != UpdateType.FixedUpdate) return;
        FollowTarget();
    }

    private void LateUpdate() {
        if(updateType != UpdateType.LateUpdate) return;
        FollowTarget();
    }

    private void FollowTarget(){
        desiredPosition = IsCloseEnough() ? desiredPosition :
            Vector2.Lerp(desiredPosition, target.position, Time.deltaTime * speed);

        transform.position = (desiredPosition + offset);
    }

    private bool IsCloseEnough(){
        return Vector2.Distance(desiredPosition,target.position) < minDistance;
    }

    private void OnValidate() {
        // FollowTarget();
    }

    private void OnDrawGizmos() {
        if(!showDebug) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(desiredPosition, minDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(desiredPosition, maxDistance);
    }
}
