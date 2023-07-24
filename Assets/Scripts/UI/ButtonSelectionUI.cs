using System.Collections;
using Management;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public sealed class ButtonSelectionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private float moveTime;
        [SerializeField] private float scaleAmount;

        private RectTransform _rectTransform;
        private Vector3 _startScale;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startScale = _rectTransform.localScale;
        }

        private IEnumerator MoveCard(bool startingAnimation)
        {
            Vector3 endScale;
            if (startingAnimation)
            {
                endScale = _startScale * scaleAmount;
            }
            else
            {
                endScale = _startScale;
            }

            for (var t = 0f; t < moveTime; t += Time.deltaTime)
            {
                Vector3 lerpScale = Vector3.Lerp(_rectTransform.localScale, endScale, t / moveTime);

                _rectTransform.localScale = lerpScale;

                yield return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject = null;
        }

        public void OnSelect(BaseEventData eventData)
        {
            StartCoroutine(MoveCard(true));

            UIManager.Instance.LastSelectedObject = gameObject;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            StartCoroutine(MoveCard(false));
        }
    }
}
