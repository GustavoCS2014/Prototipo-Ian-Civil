using Input;
using UnityEngine;

namespace Entities.TopDownPlayer
{
    public sealed class TopDownPlayerController : MonoBehaviour
    {
        [SerializeField] private TopDownPlayerSettings settings;

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 moveDirection = GameplayInput.MoveDirection;
            float moveDistance = settings.Speed * Time.deltaTime;

            bool canMove = !CircleCast(moveDirection);

            if (!canMove)
            {
                var moveDirectionX = new Vector2(moveDirection.x, 0f);
                const float perpendicularDeadZone = 0.5f;
                canMove = Mathf.Abs(moveDirection.x) > perpendicularDeadZone && !CircleCast(moveDirectionX);

                if (canMove)
                {
                    moveDirection = moveDirectionX;
                }
                else
                {
                    var moveDirectionY = new Vector2(0f, moveDirection.y);
                    canMove = Mathf.Abs(moveDirection.y) > perpendicularDeadZone && !CircleCast(moveDirectionY);

                    if (canMove)
                    {
                        moveDirection = moveDirectionY;
                    }
                }
            }

            if (canMove)
                transform.Translate(moveDirection * moveDistance);

            return;

            RaycastHit2D CircleCast(Vector2 direction) => Physics2D.CircleCast(
                origin: transform.position,
                radius: settings.Radius,
                direction: direction,
                distance: moveDistance,
                layerMask: settings.WallLayer
            );
        }
    }
}
