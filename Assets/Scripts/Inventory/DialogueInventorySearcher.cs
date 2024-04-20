using CesarJZO.DialogueSystem;
using CesarJZO.InventorySystem;
using CesarJZO.UI;
using UnityEngine;

public class DialogueInventorySearcher : MonoBehaviour {

    private DialogueManager _dialogueManager;
    [SerializeField] private Inventory inventory;

    private void Start() {
        _dialogueManager = DialogueManager.Instance;

        _dialogueManager.ConversationUpdated += OnConversationUpdated;
    }

    private void OnConversationUpdated()
    {
        if (_dialogueManager.CurrentNode is not ItemConditionalNode itemConditionalNode) return;
        
        if(inventory.HasItem(itemConditionalNode.Item)){
            InventoryUI.SelectItem(itemConditionalNode.Item);
        }else{
            InventoryUI.SelectItem(null);
        }
    }
}
