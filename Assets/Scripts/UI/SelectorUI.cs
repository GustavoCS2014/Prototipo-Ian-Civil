using Management;
using UnityEngine;

namespace UI
{
    public class SelectorUI : MonoBehaviour
    {
        [SerializeField] private Vector2 offset;

        [SerializeField] private float speed;
        [SerializeField] private float amplitude;

        private RectTransform _rectTransform;

        private void Awake()
        {
            UIManager.SelectedChanged += OnSelectedChanged;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnDestroy()
        {
            UIManager.SelectedChanged -= OnSelectedChanged;
        }

        private void OnSelectedChanged(GameObject selectedObject)
        {
            if (!selectedObject) return;

            var selectedTransform = selectedObject.GetComponent<RectTransform>();

            if (!selectedTransform) return;
            _rectTransform.position = selectedTransform.position;
            _rectTransform.sizeDelta = selectedTransform.sizeDelta + offset;
        }

        private void Update()
        {
            _rectTransform.localScale = Vector3.one * (1f + Mathf.Sin(Time.time * speed) * amplitude);
        }
    }
}
