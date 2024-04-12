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
        
        [Tooltip("Determines whether the player will know this exist or not.")]
        [SerializeField] private bool isVisible;
        public bool IsVisible => isVisible;

    }
}
