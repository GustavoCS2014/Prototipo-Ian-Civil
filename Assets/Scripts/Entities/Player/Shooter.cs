using Input;
using Units.Hazards;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    public sealed class Shooter : MonoBehaviour
    {
        [SerializeField] private bool usePlayerInput;

        [SerializeField] private Projectile projectilePrefab;

        private void Start()
        {
            if (usePlayerInput) GameplayInput.OnShoot += PlayerShoot;
        }

        private void PlayerShoot(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Launch(PlayerController.Instance.FacingDirection);
        }

        public void Shoot(Vector2 direction)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Launch(direction);
        }

        private void OnDisable()
        {
            if (usePlayerInput) GameplayInput.OnShoot -= PlayerShoot;
        }
    }
}
