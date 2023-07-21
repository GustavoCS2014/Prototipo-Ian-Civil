using UnityEngine;

namespace Input
{
    public class GameplayInput : MonoBehaviour
    {
        public static GameplayInput Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
