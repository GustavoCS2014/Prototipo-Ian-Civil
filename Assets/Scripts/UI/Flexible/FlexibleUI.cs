using UI.Flexible.Settings;
using UnityEngine;

namespace UI.Flexible
{
    [ExecuteInEditMode]
    public class FlexibleUI : MonoBehaviour
    {
        [SerializeField] protected FlexibleUISettings skinData;

        protected virtual void OnSkinUI()
        {

        }

        protected virtual void Awake()
        {
            OnSkinUI();
        }

        protected virtual void Update()
        {
            if (Application.isEditor)
            {
                OnSkinUI();
            }
        }
    }
}
