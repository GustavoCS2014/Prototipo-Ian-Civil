using System;
using Core;
using Items.Collectables;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerCollector : MonoBehaviour
    {
        public static event Action<int> CoinCollected;

        [SerializeField] private PlayerSettings settings;
        [SerializeField] private int coinAmount;

        public int CoinAmount => coinAmount;

        private Collider2D Sensor => Physics2D.OverlapBox(
            transform.position,
            transform.lossyScale,
            transform.eulerAngles.z,
            settings.CollectableLayer
        );

        private void FixedUpdate()
        {
            if (!Sensor) return;

            if (!Sensor.TryGetComponent(out ICollectable<Coin> collectable)) return;

            coinAmount += collectable.Collect().Value;
            CoinCollected?.Invoke(coinAmount);
        }

        private void OnDrawGizmosSelected()
        {
            Transform t = transform;
            Gizmos.color = Color.magenta;
            Gizmos.matrix = Matrix4x4.TRS(
                t.position,
                t.rotation,
                t.lossyScale
            );
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
