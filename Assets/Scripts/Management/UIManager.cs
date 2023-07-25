using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Management
{
    public sealed class UIManager : MonoBehaviour
    {
        public static GameObject LastSelectedObject { get; set; }

        public static void SetSelectedGameObject(GameObject selectedObject)
        {
            EventSystem.current.SetSelectedGameObject(selectedObject);
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
                EventSystem.current.SetSelectedGameObject(LastSelectedObject);
        }

        private void OnPointPerformed(InputAction.CallbackContext context)
        {
            // todo: fix this
            if (EventSystem.current.currentSelectedGameObject == LastSelectedObject)
                return;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
