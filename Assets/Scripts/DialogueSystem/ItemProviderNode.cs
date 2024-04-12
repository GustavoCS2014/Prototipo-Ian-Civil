using System.Collections;
using System.Collections.Generic;
using CesarJZO.InventorySystem;
using UnityEditor;
using UnityEngine;

namespace CesarJZO.DialogueSystem{
    public class ItemProviderNode : DialogueNode
    {
        public const string ChildProperty = nameof(child);

        [SerializeField, HideInInspector] private DialogueNode child;

        public override DialogueNode Child => child;
        
        [SerializeField] private Item giveItem;

//         public Item Item
//         {
//             get => giveItem;
// #if UNITY_EDITOR
//             set
//             {
//                 var serializedNode = new SerializedObject(this);
//                 SerializedProperty itemProperty = serializedNode.FindProperty("giveItem");
//                 itemProperty.objectReferenceValue = value;
//                 serializedNode.ApplyModifiedProperties();
//             }
// #endif
//         }

        public DialogueNode GetChild()
        {
            return child;
        }

#if UNITY_EDITOR
        public override bool TryRemoveChild(DialogueNode node)
        {
            if (child != node) return false;

            var serializedNode = new SerializedObject(this);
            SerializedProperty childProperty = serializedNode.FindProperty(ChildProperty);
            childProperty.objectReferenceValue = null;
            serializedNode.ApplyModifiedProperties();

            return true;
        }
#endif
    }
}

