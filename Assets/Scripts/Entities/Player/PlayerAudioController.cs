using Entities.Player.States;
using Media;
using UnityEngine;
using Utilities;

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
            LandState.Started += OnLandStateStarted;
        }

        private void OnDisable()
        {
            JumpState.Started -= OnJumpStateStarted;
            LandState.Started -= OnLandStateStarted;
        }

        private void OnJumpStateStarted(JumpState state)
        {
            _audioSource.PlayOneShot(audioMedia.Jump);
        }

        private void OnLandStateStarted(LandState state)
        {
            _audioSource.PlayOneShot(audioMedia.Land);
        }
    }
}
