using System;
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
            add => events.onPointerEnter.AddListener(value);
            remove => events.onPointerEnter.RemoveListener(value);
        }

        public event UnityAction PointerExited
        {
            add => events.onPointerExit.AddListener(value);
            remove => events.onPointerExit.RemoveListener(value);
        }

        public event UnityAction Selected
        {
            add => events.onSelect.AddListener(value);
            remove => events.onSelect.RemoveListener(value);
        }

        public event UnityAction Deselected
        {
            add => events.onDeselect.AddListener(value);
            remove => events.onDeselect.RemoveListener(value);
        }

        [SerializeField] private Events events;

        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            events.onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject = null;
            events.onPointerExit?.Invoke();
        }

        public void OnSelect(BaseEventData eventData)
        {
            UIManager.Instance.LastSelectedObject = gameObject;
            events.onSelect?.Invoke();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            events.onDeselect?.Invoke();
        }

        [Serializable]
        private struct Events
        {
            [SerializeField] public UnityEvent onPointerEnter;
            [SerializeField] public UnityEvent onPointerExit;
            [SerializeField] public UnityEvent onSelect;
            [SerializeField] public UnityEvent onDeselect;
        }
    }
}
