using UnityEngine;

namespace Cinematics
{
    public interface IAnimable
    {
        void ChangeState(string state);

        void FaceDirection(float direction);

        void IdleTo(Transform target);

        bool Animating { get; set; }
    }
}
