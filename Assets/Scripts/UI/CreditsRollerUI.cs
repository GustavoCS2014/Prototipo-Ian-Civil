using UnityEngine;

namespace UI
{
    public class CreditsRollerUI : MonoBehaviour
    {
        [SerializeField] private float speed;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            _rectTransform.Translate(Vector3.up * (Time.deltaTime * speed));
        }
    }
}
