using Entities;
using Entities.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private HurtBox entityHurtBox;
        [SerializeField] private BaseEntitySettings entitySettings;
        [SerializeField] private Image type;

        private void Start()
        {
            if (entityHurtBox)
                entityHurtBox.DamageTaken += OnDamageTaken;
        }

        private void OnDestroy()
        {
            if (entityHurtBox)
                entityHurtBox.DamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken(int amount)
        {
            if (entitySettings)
                type.fillAmount = (float)entityHurtBox.Health / entitySettings.MaxHealth;
        }
    }
}
