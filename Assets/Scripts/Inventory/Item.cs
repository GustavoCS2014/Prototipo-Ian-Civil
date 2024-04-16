using System.ComponentModel;
using UnityEngine;

namespace CesarJZO.InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 1)]
    public class Item : ScriptableObject
    {
        [Tooltip("The name shown in the UI.")]
        [SerializeField] private string displayName;
        public string DisplayName => displayName;

        [Tooltip("The sprite shown in the UI.")]
        [SerializeField] private Sprite displaySprite;
        public Sprite DisplaySprite => displaySprite;
        
        [Tooltip("Determines if this is a hidden item used to change dialogues.")]
        [SerializeField] private bool isKey;
        public bool IsKey => isKey;

    }
}
