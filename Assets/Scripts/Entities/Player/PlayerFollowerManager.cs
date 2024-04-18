using System.Collections.Generic;
using Entities.Follower;
using UnityEngine;
    
namespace Entities.Player{
    public class PlayerFollowerManager : MonoBehaviour {
        
        [field: SerializeField] public List<GameObject> Followers {get; private set;}
        [SerializeField] private List<GameObject> FollowerPrefabs;
        


        //Should move this to other script in the oficial project.
        public void AddFollower(FollowerController follower){
            //if that follower is following the player already do nothing.
            if(Followers.Contains(follower.gameObject)) return;
            Followers.Add(follower.gameObject);            
              
            Debug.Log($"TryAdd {follower}, {FollowerPrefabs.Contains(follower.gameObject)}");
        } 
        public void RemoveFollower(FollowerController follower) {
            //if that follower isn't following the player, do nothing.
            Debug.Log($"TryRemove {follower}");
            if(!Followers.Contains(follower.gameObject)) return;
            Followers.Remove(follower.gameObject);              
        } 

    }
}