using Management;
using UnityEngine;

namespace UI
{
    public class AnimatedSelectorUI : MonoBehaviour
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private float baseScale;

        [SerializeField] private float transitionSpeed;
        [SerializeField] private float scaleSpeed;
        [SerializeField] private float amplitude;

        private RectTransform _rectTransform;
        private Vector2 _targetSizeDelta;
        private Vector3 _targetPosition;

        private void Awake()
        {
            UIManager.SelectedChanged += OnSelectedChanged;
            _rectTransform = GetComponent<RectTransform>();

            _targetSizeDelta = _rectTransform.sizeDelta;
            _targetPosition = _rectTransform.position;
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
            _targetPosition = selectedTransform.position;
            _targetSizeDelta = selectedTransform.sizeDelta + offset;
        }

        private void Update()
        {
            _rectTransform.position = Vector3.Lerp(_rectTransform.position, _targetPosition, transitionSpeed * Time.unscaledDeltaTime);
            _rectTransform.sizeDelta = Vector2.Lerp(_rectTransform.sizeDelta, _targetSizeDelta, transitionSpeed * Time.unscaledDeltaTime);
            _rectTransform.localScale = Vector3.one * (baseScale + Mathf.Sin(Time.unscaledTime * scaleSpeed) * amplitude);
        }
    }
}
