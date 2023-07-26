using System;

namespace Core
{
    public interface IHasProgress
    {
        event Action<float> ProgressUpdated;

        float Progress { get; }
    }
}
