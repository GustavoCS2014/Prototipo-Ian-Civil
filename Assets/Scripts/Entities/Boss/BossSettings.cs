using UnityEngine;
using Utilities;

namespace Entities.Boss
{
    [CreateAssetMenu(fileName = "Boss Settings", menuName = "Bosses/Boss Settings")]
    public class BossSettings : ScriptableObject
    {
        [SerializeField] private Range idleTime;
        public Range IdleTime => idleTime;
    }
}
