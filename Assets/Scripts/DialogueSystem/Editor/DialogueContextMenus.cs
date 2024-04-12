﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CesarJZO.DialogueSystem.Editor
{
    public static class DialogueContextMenus
    {
        public static void AddRightClickNodeItems(this GenericMenu menu, DialogueNode node, DialogueEditor editor)
        {
            menu.AddItem(new GUIContent("Set Node as Root"), false,
                () => editor.SelectedDialogue.SetNodeAsRoot(node)
            );
            menu.AddItem(new GUIContent("Delete Node"), false,
                () =>
                {
                    int index = editor.SelectedDialogue.IndexOf(node);

                    var serializedDialogue = new SerializedObject(editor.SelectedDialogue);
                    SerializedProperty nodesProperty = serializedDialogue.FindProperty("nodes");
                    nodesProperty.DeleteArrayElementAtIndex(index);
                    serializedDialogue.ApplyModifiedPropertiesWithoutUndo();

                    foreach (DialogueNode n in editor.SelectedDialogue.Nodes)
                        n.TryRemoveChild(node);

                    AssetDatabase.RemoveObjectFromAsset(node);
                    AssetDatabase.SaveAssets();
                }
            );
        }

        public static void AddNodeMenuItems(this GenericMenu menu, Vector2 position, DialogueEditor editor)
        {
            menu.AddItem(new GUIContent("Add Simple Node"), false,
                () => editor.SelectedDialogue.CreateNodeAtPoint(DialogueNodeType.SimpleNode, position)
            );
            menu.AddItem(new GUIContent("Add Response Node"), false,
                () => editor.SelectedDialogue.CreateNodeAtPoint(DialogueNodeType.ResponseNode, position)
            );
            menu.AddItem(new GUIContent("Add Item Conditional Node"), false,
                () => editor.SelectedDialogue.CreateNodeAtPoint(DialogueNodeType.ConditionalNode, position)
            );
            DialogueNode rootNode = editor.SelectedDialogue.RootNode;
            if (rootNode)
                menu.AddItem(new GUIContent("Go To Root Node"), false, () => editor.ScrollToNode(rootNode));
        }

        public static void AddSimpleNodeMenuItems(this GenericMenu menu, SimpleNode simpleNode, DialogueEditor editor)
        {
            menu.AddItem(new GUIContent("Add Node/Add Simple Node"), false,
                () => editor.SelectedDialogue.AddChildToSimpleNode(simpleNode, DialogueNodeType.SimpleNode)
            );
            menu.AddItem(new GUIContent("Add Node/Add Conditional Node"), false,
                () => editor.SelectedDialogue.AddChildToSimpleNode(simpleNode, DialogueNodeType.ConditionalNode)
            );
            menu.AddItem(new GUIContent("Add Node/Add Response Node"), false,
                () => editor.SelectedDialogue.AddChildToSimpleNode(simpleNode, DialogueNodeType.ResponseNode)
            );
            menu.AddItem(new GUIContent("Link Node"), false,
                () => editor.LinkingNode = new NodeContext(simpleNode)
            );
        }

        public static void AddConditionalNodeMenuItems(this GenericMenu menu, ItemConditionalNode conditionalNode, bool which, DialogueEditor editor)
        {
            menu.AddItem(new GUIContent("Add Node/Add Simple Node"), false,
                () => editor.SelectedDialogue.AddChildToConditionalNode(conditionalNode, DialogueNodeType.SimpleNode, which)
            );

            menu.AddItem(new GUIContent("Add Node/Add Response Node"), false,
                () => editor.SelectedDialogue.AddChildToConditionalNode(conditionalNode, DialogueNodeType.ResponseNode, which)
            );
            menu.AddItem(new GUIContent("Add Node/Add Conditional Node"), false,
                () => editor.SelectedDialogue.AddChildToConditionalNode(conditionalNode, DialogueNodeType.ConditionalNode,
                    which)
            );
            menu.AddItem(new GUIContent("Link node"), false,
                () => editor.LinkingNode = new NodeContext(conditionalNode) { valueIfConditional = which }
            );
        }

        public static void AddResponseNodeMenuItems(this GenericMenu menu, ResponseNode responseNode, int index, DialogueEditor editor)
        {
            menu.AddItem(new GUIContent("Add Node/Add Simple Node"), false,
                () => editor.SelectedDialogue.AddChildToResponseNode(responseNode, DialogueNodeType.SimpleNode, index)
            );

            menu.AddItem(new GUIContent("Add Node/Add Response Node"), false,
                () => editor.SelectedDialogue.AddChildToResponseNode(responseNode, DialogueNodeType.ResponseNode, index)
            );
            menu.AddItem(new GUIContent("Add Node/Add Conditional Node"), false,
                () => editor.SelectedDialogue.AddChildToResponseNode(responseNode, DialogueNodeType.ConditionalNode, index)
            );
            menu.AddItem(new GUIContent("Link node"), false,
                () => editor.LinkingNode = new NodeContext(responseNode) { indexIfResponse = index }
            );
        }
    }
}
#endif
