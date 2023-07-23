using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public struct Range
    {
        // [HideInInspector]
        [SerializeField]
        private float min;

        // [HideInInspector]
        [SerializeField]
        private float max;

        public float Min
        {
            get => min;
            set => min = value;
        }

        public float Max
        {
            get => max;
            set => max = value;
        }

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public float RandomValue => UnityEngine.Random.Range(min, max);

        public float Clamp(float value) => Mathf.Clamp(value, min, max);

        public float Lerp(float t) => Mathf.Lerp(min, max, t);
    }
}
