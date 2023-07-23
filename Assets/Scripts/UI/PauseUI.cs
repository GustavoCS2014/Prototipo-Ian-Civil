using Management;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button exitToTitleButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            if (resumeButton)
                resumeButton.onClick.AddListener(PauseManager.Instance.Resume);
            if (optionsButton)
                optionsButton.onClick.AddListener(() => Debug.Log("Options"));
            if (exitToTitleButton)
                exitToTitleButton.onClick.AddListener(() =>
                {
                    PauseManager.Instance.Resume();
                    SceneManager.LoadScene(GameScene.MainMenu);
                });
            if (quitButton)
                quitButton.onClick.AddListener(Application.Quit);

            PauseManager.Paused += OnPause;
            PauseManager.Resumed += OnResume;

            OnResume();
        }

        private void OnPause()
        {
            gameObject.SetActive(true);
        }

        private void OnResume()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            PauseManager.Paused -= OnPause;
            PauseManager.Resumed -= OnResume;
        }
    }
}
