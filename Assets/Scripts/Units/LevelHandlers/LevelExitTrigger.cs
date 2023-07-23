using Management;
using UnityEngine;

namespace Units.LevelHandlers
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelExitTrigger : MonoBehaviour
    {
        [SerializeField] private GameScene nextScene;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                SceneManager.LoadScene(nextScene);
        }
    }
}
