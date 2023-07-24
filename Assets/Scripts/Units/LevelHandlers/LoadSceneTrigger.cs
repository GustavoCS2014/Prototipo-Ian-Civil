using Core;
using Management;
using UnityEngine;

namespace Units.LevelHandlers
{
    public sealed class LoadSceneTrigger : MonoBehaviour
    {
        [SerializeField] private GameState loadSceneIfState;

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
            if (loadSceneIfState != 0)
                GameManager.StateChanged += OnGameStateChange;
        }

        private void OnGameStateChange(GameState state)
        {
            if (state.HasFlag(loadSceneIfState))
                LoadNextScene();
        }

        private void OnDestroy()
        {
            if (loadSceneIfState != 0)
                GameManager.StateChanged -= OnGameStateChange;
        }
    }
}
