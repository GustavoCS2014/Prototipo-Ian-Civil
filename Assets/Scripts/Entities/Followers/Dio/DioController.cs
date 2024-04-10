using Entities.Player;
using UnityEngine;

namespace Entities.Follower.Dio{
    public class DioController : FollowerController, IFollowerInteractable {
        public Vector3 InteractablePosition(){
            return transform.position;
        }

        public void Interaction(PlayerController interactor){}

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
