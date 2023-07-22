using System.Collections;
using UnityEngine;

namespace Entities
{
    public sealed class DamageRenderer : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float defaultTime;
        [SerializeField] private Color damageColor;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private HurtBox hurtBox;

        private static readonly Color NeutralColor = Color.white;

        private float _time;

        private void Start()
        {
            _time = hurtBox.DamageTime > 0f ? hurtBox.DamageTime : defaultTime;
            hurtBox.DamageTaken += OnDamageTaken;
        }

        private void OnDamageTaken(int i)
        {
            StopAllCoroutines();
            StartCoroutine(LerpColor());
        }

        private IEnumerator LerpColor()
        {
            for (var t = 0f; t < _time; t += Time.deltaTime)
            {
                spriteRenderer.color = Color.Lerp(damageColor, NeutralColor, Utilities.Ease.InOutSine(t));
                yield return null;
            }
        }
    }
}
