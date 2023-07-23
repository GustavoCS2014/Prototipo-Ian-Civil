using UnityEngine;

namespace Utilities
{
    public static class PhysicsCalculator
    {
        public static float JumpForce(float gravityScale, float jumpHeight, float mass)
        {
            float gravityScaled = Physics2D.gravity.y * gravityScale;
            return Mathf.Sqrt(-2f * gravityScaled * jumpHeight) * mass;
        }

        public static float JumpForce(Rigidbody2D rigidbody, float jumpHeight)
        {
            return JumpForce(rigidbody.gravityScale, jumpHeight, rigidbody.mass);
        }
    }
}
