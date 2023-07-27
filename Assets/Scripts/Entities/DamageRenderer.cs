using System.Collections;
using UnityEngine;

namespace Entities
{
    public sealed class DamageRenderer : MonoBehaviour
    {
        [SerializeField] private Color damageColor;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private HurtBox hurtBox;

        private Color _neutralColor;

        private void Start()
        {
            _neutralColor = spriteRenderer.color;
            hurtBox.DamageTaken += OnDamageTaken;
        }

        private void OnDamageTaken(int i)
        {
            StopAllCoroutines();
            StartCoroutine(LerpColor());
        }

        private IEnumerator LerpColor()
        {
            for (var t = 0f; t < hurtBox.DamageTime * 1.1; t += Time.deltaTime)
            {
                spriteRenderer.color = Color.Lerp(damageColor, _neutralColor, Utilities.Ease.InOutSine(t));
                yield return null;
            }
            spriteRenderer.color = _neutralColor;
        }
    }
}
