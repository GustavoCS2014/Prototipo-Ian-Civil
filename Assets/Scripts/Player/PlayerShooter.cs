using Input;
using Items.MonoBehaviours;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public sealed class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;

        private void Start()
        {
            GameplayInput.OnShoot += Shoot;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Launch(PlayerController.Instance.FacingDirection);
        }

        private void OnDisable()
        {
            GameplayInput.OnShoot -= Shoot;
        }
    }
}
