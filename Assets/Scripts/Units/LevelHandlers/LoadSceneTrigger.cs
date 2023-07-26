using Core;
using Management;
using UnityEngine;

namespace Units.LevelHandlers
{
    public sealed class LoadSceneTrigger : MonoBehaviour
    {
        [SerializeField] private GameScene nextScene;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                LoadNextScene();
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
