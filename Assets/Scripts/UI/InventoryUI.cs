using System;
using System.Collections.Generic;
using Attributes;
using CesarJZO.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace CesarJZO.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public static event Action<Item> ItemSelected;
        [SerializeField] private Item debugItem;
        [SerializeField] private Inventory inventory;
        [SerializeField] private Inventory allItems;
        [SerializeField] private List<Item> currentItems;
        [SerializeField] private Transform itemTemplate;
        [SerializeField] private Transform inventoryContainer;

        private void Start()
        {
            Quit();
        }

        private void Update(){
            // if(inventory.CountChanged())
            //     UpdateItems();

        }

        private void OnGUI() {
            if(GUILayout.Button("sendItem", GUILayout.Width(100f), GUILayout.Height(50f))){
                SelectItem(debugItem);
            }
        }

        public void Open()
        {
            UpdateItems();
            gameObject.SetActive(true);
        }

        public void Quit()
        {
            gameObject.SetActive(false);
        }

        public void SelectItem(Item item)
        {
            ItemSelected?.Invoke(item);
        }

        private void UpdateItems(){
            foreach(Transform child in inventoryContainer){
                Destroy(child.gameObject);
            }
            currentItems.Clear();


            //? if the item is visible and is on the player's inventory, show it.
            foreach(Item item in allItems.Items){
                if(!item.IsVisible) continue;
                if(!inventory.HasItem(item)) continue;

                currentItems.Add(item);
            }

            //? show the items on screen.
            foreach (Item item in currentItems){
                Transform itemTransform = Instantiate(itemTemplate, inventoryContainer);
                itemTransform.gameObject.SetActive(true);
                itemTransform.GetComponent<Image>().sprite = item.DisplaySprite;
            }
        }

        public bool SearchItem(Item item){
            return inventory.HasItem(item);
        }

    }
}
