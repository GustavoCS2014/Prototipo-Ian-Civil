namespace Cinematics
{
    public interface IAnimable
    {
        void ChangeState(string state);

        void FaceDirection(float direction);

        bool Animating { get; set; }
    }
}
