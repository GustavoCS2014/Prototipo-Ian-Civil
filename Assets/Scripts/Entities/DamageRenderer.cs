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
            StartCoroutine(LerpColor());
        }

        private IEnumerator LerpColor()
        {
            for (float t = 0; t < _time; t += Time.deltaTime)
            {
                spriteRenderer.color = Color.Lerp(damageColor, NeutralColor, Utilities.Ease.InOutSine(t));
                yield return null;
            }
        }
    }
}
