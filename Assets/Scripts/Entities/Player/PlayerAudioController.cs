using Entities.Player.States;
using Media;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] private EntityAudioMedia audioMedia;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            JumpState.Started += OnJumpStateStarted;
        }

        private void OnDisable()
        {
            JumpState.Started -= OnJumpStateStarted;
        }

        private void OnJumpStateStarted(JumpState state)
        {
            _audioSource.PlayOneShot(audioMedia.Jump);
        }
    }
}
