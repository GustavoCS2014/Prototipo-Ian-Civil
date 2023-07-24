using Core;
using Management;
using UnityEngine;

namespace Units.LevelHandlers
{
    public sealed class LoadSceneTrigger : MonoBehaviour
    {
        [SerializeField] private GameState loadSceneIf;

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

        private void Start()
        {
            if (loadSceneIf != 0)
                GameManager.StateChanged += OnGameStateChange;
        }

        private void OnGameStateChange(GameState state)
        {
            if (state.HasFlag(loadSceneIf))
                LoadNextScene();
        }

        private void OnDestroy()
        {
            if (loadSceneIf != 0)
                GameManager.StateChanged -= OnGameStateChange;
        }
    }
}
