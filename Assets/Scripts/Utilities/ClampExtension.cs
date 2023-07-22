using UnityEngine;

namespace Utilities
{
    public static class ClampExtension
    {
        public static float Clamp01(this float value)
        {
            return Mathf.Clamp01(value);
        }
    }
}
