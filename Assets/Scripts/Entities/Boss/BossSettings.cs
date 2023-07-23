using UnityEngine;
using Utilities;

namespace Entities.Boss
{
    [CreateAssetMenu(fileName = "Boss Settings", menuName = "Bosses/Boss Settings")]
    public class BossSettings : BaseEntitySettings
    {
        [SerializeField] private Range idleTime;
        public Range IdleTime => idleTime;
    }
}
