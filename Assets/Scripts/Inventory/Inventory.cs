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

        private int _lastCount;
        public bool CountChanged(){
            if(_lastCount != items.Count){
                _lastCount = items.Count;
                return true;
            }
            return false;
        }
    }
}
