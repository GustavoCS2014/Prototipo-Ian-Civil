using System;
using System.Collections.Generic;
using UnityEngine;

namespace CesarJZO.InventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        [SerializeField] private List<Item> items;
        public List<Item> Items => items;

        public bool HasItem(Item item)
        {
            return items.Contains(item);
        }

        public void AddItem(Item item){
            items.Add(item);
        }
        public void RemoveItem(Item item){
            items.Remove(item);
        }
    }
}
