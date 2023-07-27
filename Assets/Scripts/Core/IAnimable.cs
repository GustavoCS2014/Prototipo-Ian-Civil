namespace Core
{
    public interface IAnimable
    {
        void ChangeState(string state);

        void FaceDirection(float direction);
    }
}
