using System;
using System.Collections.Generic;
using UnityEngine;

namespace CesarJZO.InventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        //? Maybe change the inventory to a dictionary <Item, bool>?
        [SerializeField] private Item[] items;
        [SerializeField] private bool[] itemsAdquiered;
        public bool[] ItemsAdquiered => itemsAdquiered;

        public bool HasItem(Item item)
        {
            return itemsAdquiered[GetItemIndex(item)];
        }

        public void AddItem(Item item){
            itemsAdquiered[GetItemIndex(item)] = true;
        }

        public void RemoveItem(Item item){
            itemsAdquiered[GetItemIndex(item)] = false;
        }

        public int GetItemCount() => items.Length;


        /// <summary>
        /// Checks if the player has this item
        /// </summary>
        /// <param name="index">The index of the item (for use in for loops)</param>
        /// <param name="item">output item property</param>
        /// <returns></returns>
        public bool TryGetItemAtIndex(int index, out Item item){
            item = null;
            if(HasItem(items[index])){
                item = items[index];
                return true;
            }
            return false;
        }

        private int GetItemIndex(Item requieredItem){
            for(int i = 0; i < items.Length; i++){
                if(requieredItem == items[i]) return i;
            }
            return -1;
        }
    }
}
