using System;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Management
{
    public sealed class UIManager : MonoBehaviour
    {
        public static event Action<GameObject> SelectedChanged;

        private static GameObject _lastSelectedObject;

        public static GameObject LastSelectedObject
        {
            get => _lastSelectedObject;
            set
            {
                if (_lastSelectedObject == value) return;
                _lastSelectedObject = value;
                SelectedChanged?.Invoke(value);
            }
        }

        public static void SetSelectedGameObject(GameObject selectedObject)
        {
            EventSystem.current.SetSelectedGameObject(selectedObject);
            SelectedChanged?.Invoke(selectedObject);
        }

        private void Awake()
        {
            UIInput.NavigatePerformed += OnNavigatePerformed;
            UIInput.PointPerformed += OnPointPerformed;
        }

        private void OnDestroy()
        {
            UIInput.NavigatePerformed -= OnNavigatePerformed;
            UIInput.PointPerformed -= OnPointPerformed;
        }


        private void OnNavigatePerformed(InputAction.CallbackContext context)
        {
            if (context.performed)
                SetSelectedGameObject(LastSelectedObject);
        }

        private void OnPointPerformed(InputAction.CallbackContext context)
        {
            // todo: fix this
            if (EventSystem.current.currentSelectedGameObject == LastSelectedObject)
                return;
            SetSelectedGameObject(null);
        }
    }
}
