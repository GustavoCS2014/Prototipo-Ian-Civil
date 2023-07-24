using TMPro;
using UnityEngine;

namespace UI
{
    public class PopUpMessageUI : MonoBehaviour
    {
        [SerializeField] private string message;
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            text.text = message;
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
