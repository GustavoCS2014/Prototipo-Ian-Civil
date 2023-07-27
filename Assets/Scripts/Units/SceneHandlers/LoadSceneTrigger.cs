using Core;
using Management;
using UnityEngine;

namespace Units.LevelHandlers
{
    public sealed class LoadSceneTrigger : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float delay;
        [SerializeField] private GameScene nextScene;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                LoadNextScene();
        }

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
