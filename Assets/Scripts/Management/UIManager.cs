using UnityEngine;
using UnityEngine.EventSystems;

namespace Management
{
    public sealed class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public GameObject LastSelectedObject { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SetSelectedObject(GameObject selectedObject)
        {
            EventSystem.current.SetSelectedGameObject(selectedObject);
        }
    }
}
