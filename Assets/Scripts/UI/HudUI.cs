using System;
using Entities.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HudUI : MonoBehaviour
    {
        private const char MultiplierSymbol = '\u00d7'; // ×
        [SerializeField] private TextMeshProUGUI coinsText;

        private void Start()
        {
            PlayerCollector.CoinCollected += OnCoinCollected;
            coinsText.text = $"{MultiplierSymbol}{PlayerCollector.Instance.CoinAmount:D2}";
        }

        private void OnCoinCollected(int amount)
        {
            coinsText.text = $"\u00d7{PlayerCollector.Instance.CoinAmount:D2}";
        }

        private void OnDestroy()
        {
            PlayerCollector.CoinCollected -= OnCoinCollected;
        }
    }
}
