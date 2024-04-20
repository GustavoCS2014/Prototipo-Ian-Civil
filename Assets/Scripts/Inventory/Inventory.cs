using System;
using System.Collections.Generic;
using CesarJZO.InventorySystem;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

namespace CesarJZO.InventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        public static event Action OnItemsChanged;

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
            OnItemsChanged?.Invoke();
        }

        public void RemoveItem(Item item){
            itemsAdquiered[GetItemIndex(item)] = false;
            OnItemsChanged?.Invoke();
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

        private void OnValidate() {
            if(items.Length == itemsAdquiered.Length) return;
            bool[] tempArray = (bool[])itemsAdquiered.Clone();
            itemsAdquiered = new bool[items.Length];
            for(int i = 0; i < tempArray.Length; i++){
                if(i >= ItemsAdquiered.Length) return;
                ItemsAdquiered[i] = tempArray[i];
            }
        }

    }
}
