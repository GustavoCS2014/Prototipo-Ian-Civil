using UnityEngine;

namespace Media
{
    [CreateAssetMenu(fileName = "Entity Animations Media", menuName = "Media/Animations")]
    public sealed class EntityAnimationsMedia : ScriptableObject
    {
        [SerializeField] private AnimationClip idle;
        public AnimationClip Idle => idle;

        [SerializeField] private AnimationClip jump;
        public AnimationClip Jump => jump;

        [SerializeField] private AnimationClip death;
        public AnimationClip Death => death;
    }
}
