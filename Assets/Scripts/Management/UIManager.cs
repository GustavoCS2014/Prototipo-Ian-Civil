using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Management
{
    public sealed class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public GameObject LastSelectedObject { get; set; }

        public void SetSelectedObject(GameObject selectedObject)
        {
            EventSystem.current.SetSelectedGameObject(selectedObject);
        }

        private void Awake()
        {
            Instance = this;
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
