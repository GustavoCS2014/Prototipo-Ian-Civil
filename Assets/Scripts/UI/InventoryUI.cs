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
        [SerializeField] private Inventory inventory;
        [SerializeField] private Transform inventoryContainer;
        [SerializeField] private Transform itemTemplate;

        private void Start()
        {
            Quit();
        }

        public void Open()
        {
            ShowAdquieredItems();
            gameObject.SetActive(true);
        }

        public void Quit()
        {
            gameObject.SetActive(false);
        }

        public void UpdateItems(){
            ShowAdquieredItems();
        }


        /// <summary>
        /// Invokes the event <see cref="ItemSelected"/>
        /// </summary>
        /// <param name="item"></param>
        public void SelectItem(Item item)
        {
            ItemSelected?.Invoke(item);
        }



        private void ShowAdquieredItems(){
            foreach(Transform child in inventoryContainer){
                Destroy(child.gameObject);
            }

            for(int i = 0; i < inventory.GetItemCount(); i++){
                if(inventory.TryGetItemAtIndex(i, out Item item)){
                    //Ignores invisible items(keys)
                    if(item.IsKey) continue;

                    Transform itemTransform = Instantiate(itemTemplate, inventoryContainer);
                    itemTransform.gameObject.SetActive(true);
                    itemTransform.GetComponent<Image>().sprite = item.DisplaySprite;
                }

            }
        }


        /// <summary>
        /// Checks if the player has the item in his inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchItem(Item item){
            return inventory.HasItem(item);
        }
    }
}
