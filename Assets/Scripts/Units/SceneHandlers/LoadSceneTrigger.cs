using Core;
using Management;
using UnityEngine;

namespace Units.SceneHandlers
{
    public sealed class LoadSceneTrigger : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float delay;
        [SerializeField] private GameScene nextScene;

        public void LoadNextScene()
        {
            Invoke(nameof(LoadNextSceneWithDelay), delay);
        }

        private void LoadNextSceneWithDelay()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
