using System.Collections;
using Input;
using Management;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI
{
    public sealed class ButtonSelectionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private float verticalMoveAmount;
        [SerializeField] private float moveTime;
        [SerializeField] private float scaleAmount;

        private Vector3 _startPosition;
        private Vector3 _startScale;

        private void Start()
        {
            _startPosition = transform.position;
            _startScale = transform.localScale;

            UIInput.Navigate += OnNavigate;
            UIInput.PointPerformed += OnPointPerformed;
        }

        private void OnDestroy()
        {
            UIInput.Navigate -= OnNavigate;
            UIInput.PointPerformed -= OnPointPerformed;
        }

        private void OnNavigate(InputAction.CallbackContext context)
        {
            if (context.performed)
                EventSystem.current.SetSelectedGameObject(UIManager.Instance.LastSelectedObject);
        }

        private void OnPointPerformed(InputAction.CallbackContext context)
        {
            if (EventSystem.current.currentSelectedGameObject == UIManager.Instance.LastSelectedObject)
                return;
            EventSystem.current.SetSelectedGameObject(null);
        }

        private IEnumerator MoveCard(bool startingAnimation)
        {
            Vector3 endPosition;
            Vector3 endScale;

            for (var t = 0f; t < moveTime; t += Time.deltaTime)
            {
                if (startingAnimation)
                {
                    endPosition = _startPosition + Vector3.up * verticalMoveAmount;
                    endScale = _startScale * scaleAmount;
                }
                else
                {
                    endPosition = _startPosition;
                    endScale = _startScale;
                }

                Vector3 lerpPosition = Vector3.Lerp(transform.position, endPosition, t / moveTime);
                Vector3 lerpScale = Vector3.Lerp(transform.localScale, endScale, t / moveTime);

                transform.position = lerpPosition;
                transform.localScale = lerpScale;

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
