using Input;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public sealed class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Projectile projectileInfo;

        private void Start()
        {
            GameplayInput.OnShoot += Shoot;
        }

        private void Shoot(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                Projectile.Spawn(projectileInfo, transform.position, transform.rotation);
        }

        private void OnDisable()
        {
            GameplayInput.OnShoot -= Shoot;
        }
    }
}
