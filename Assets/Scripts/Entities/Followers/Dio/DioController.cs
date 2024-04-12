using Entities.Player;
using UnityEngine;

namespace Entities.Follower.Dio{
    public class DioController : FollowerController, IFollowerInteractable {
        public Vector3 GetPosition(){
            return transform.position;
        }

        public void Interact(){}

        public void FollowerInteraction(PlayerController interactor, out FollowerController interactable){
            interactable = this;
            if(Target != null){
                DeleteTarget();
                return;
            }
            SetTarget(interactor.transform);
        }
    }
}
