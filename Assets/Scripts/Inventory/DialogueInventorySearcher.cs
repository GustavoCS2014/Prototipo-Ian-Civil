using CesarJZO.DialogueSystem;
using CesarJZO.InventorySystem;
using CesarJZO.UI;
using UnityEngine;

public class DialogueInventorySearcher : MonoBehaviour {

    private DialogueManager _dialogueManager;
    [SerializeField] private InventoryUI inventoryUI;
    [Tooltip("This item will be used when the player doesn't have the requiered item to change branches.")]
    [SerializeField] private Item defaultItem;

    private void Start() {
        _dialogueManager = DialogueManager.Instance;

        _dialogueManager.ConversationUpdated += OnConversationUpdated;
    }

    private void OnConversationUpdated()
    {
        if (_dialogueManager.CurrentNode is not ItemConditionalNode itemConditionalNode) return;
        
        if(inventoryUI.SearchItem(itemConditionalNode.Item)){
            inventoryUI.SelectItem(itemConditionalNode.Item);
        }else{
            inventoryUI.SelectItem(defaultItem);
        }
    }
}
