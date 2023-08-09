using UnityEngine;

namespace Media
{
    [CreateAssetMenu(fileName = "Entity Audio Media", menuName = "Media/Audio")]
    public sealed class EntityAudioMedia : ScriptableObject
    {
        [SerializeField] private AudioClip jump;
        public AudioClip Jump => jump;

        [SerializeField] private AudioClip death;
        public AudioClip Death => death;

        [SerializeField] private AudioClip shoot;
        public AudioClip Shoot => shoot;
    }
}
