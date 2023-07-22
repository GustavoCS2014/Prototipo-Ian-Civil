using System;
using System.Collections;
using Core;
using UnityEngine;

namespace Units.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class WallButton : MonoBehaviour, IDamageTaker
    {
        public event Action Activated;
        public event Action Deactivated;

        [SerializeField] private bool active;
        [SerializeField] private bool permanent;

        public bool Active => active;

        public float ActiveTime { get; set; }

        public void TakeDamage(int damage)
        {
            if (permanent) return;
            StopAllCoroutines();
            StartCoroutine(ActivateForSeconds());
        }

        private IEnumerator ActivateForSeconds()
        {
            active = true;
            Activated?.Invoke();
            yield return new WaitForSeconds(ActiveTime);
            active = false;
            Deactivated?.Invoke();
        }

        public void SetPermanent()
        {
            StopAllCoroutines();
            permanent = true;
        }
    }
}
