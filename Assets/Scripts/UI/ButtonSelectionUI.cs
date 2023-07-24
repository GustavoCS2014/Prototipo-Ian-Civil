using Management;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public sealed class ButtonSelectionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        public event UnityAction PointerEntered
        {
            add => onPointerEnter.AddListener(value);
            remove => onPointerEnter.RemoveListener(value);
        }

        public event UnityAction PointerExited
        {
            add => onPointerExit.AddListener(value);
            remove => onPointerExit.RemoveListener(value);
        }

        public event UnityAction Selected
        {
            add => onSelect.AddListener(value);
            remove => onSelect.RemoveListener(value);
        }

        public event UnityAction Deselected
        {
            add => onDeselect.AddListener(value);
            remove => onDeselect.RemoveListener(value);
        }

        [SerializeField] private UnityEvent onPointerEnter;
        [SerializeField] private UnityEvent onPointerExit;
        [SerializeField] private UnityEvent onSelect;
        [SerializeField] private UnityEvent onDeselect;

        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject = null;
            onPointerExit?.Invoke();
        }

        public void OnSelect(BaseEventData eventData)
        {
            UIManager.Instance.LastSelectedObject = gameObject;
            onSelect?.Invoke();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            onDeselect?.Invoke();
        }
    }
}
