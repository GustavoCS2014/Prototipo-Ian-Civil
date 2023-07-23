using Management;
using UnityEngine;

namespace Units.LevelHandlers
{
    public class LevelExitTrigger : MonoBehaviour
    {
        [SerializeField] private GameScene nextScene;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                LoadScene();
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
