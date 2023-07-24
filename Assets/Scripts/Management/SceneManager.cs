using System.Collections;
using Core;
using UnityEngine;

namespace Management
{
    public sealed class SceneManager : MonoBehaviour
    {
        [SerializeField] private GameScene defaultNextScene;

        public static void LoadScene(GameScene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }

        public void LoadSceneAfterDelay(float delay)
        {
            StartCoroutine(LoadSceneCoroutine());

            IEnumerator LoadSceneCoroutine()
            {
                yield return new WaitForSeconds(delay);
                LoadScene(defaultNextScene);
            }
        }
    }
}
