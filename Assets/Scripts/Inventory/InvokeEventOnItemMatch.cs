using System.Collections.Generic;
using CesarJZO.InventorySystem;
using UnityEngine;
using UnityEngine.Events;

public class InvokeEventOnItemMatch : MonoBehaviour {

    public enum CheckOn{
        Start,
        Update,
        WhenCalled,
    }
    [Tooltip("When to check\nStart = On Scene Load \nUpdate = Update Loop \n WhenCalled = Will not execute until called with a unity event.")]
    [SerializeField] private CheckOn checkOn;
    [SerializeField] private Inventory inventory;

    [Space(10)]
    [Header("Events")]
    [SerializeField] private Item itemToCheck;
    [SerializeField] private UnityEvent onItemMatch;
    [SerializeField] private UnityEvent onItemMismatch;

    [SerializeField] private Item[] itemsToCheck;
    [SerializeField] private UnityEvent onItemsMatch;
    [SerializeField] private UnityEvent onItemsMismatch;

    private void Start() {
        if(checkOn != CheckOn.Start) return;
        if(itemToCheck is not null){
            CheckPlayerItem(itemToCheck);
        }
        if(itemsToCheck is not null){
            CheckPlayerItems(itemsToCheck);
        }
    }

    private void Update(){
        if(checkOn != CheckOn.Update) return;
        if(itemToCheck is not null){
            CheckPlayerItem(itemToCheck);
        }
        if(itemsToCheck is not null){
            CheckPlayerItems(itemsToCheck);
        }

    }

    /// <summary>
    /// if the player has the item provided, trow the onPlayerHasItem unity event.
    /// </summary>
    /// <param name="item">The item to check</param>
    public void CheckPlayerItem(Item item){
        if(inventory.HasItem(item)){
            onItemMatch?.Invoke();
            return;
        }
        onItemMismatch?.Invoke();
    }

    /// <summary>
    /// If the player has all items in the array, trow the onPlayerHasAllItems unity event.
    /// </summary>
    /// <param name="itemArray">The array of items to be checked.</param>
    public void CheckPlayerItems(Item[] itemArray){
        foreach(Item item in itemArray){
            if(!inventory.HasItem(item)){
                onItemsMismatch?.Invoke();
                return;
            }
        }
        onItemsMatch?.Invoke();
    }
}