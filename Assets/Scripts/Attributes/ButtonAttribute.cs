using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Attributes
{
    [Serializable]
    public struct InspectorButton { }

    public class ButtonAttribute : PropertyAttribute
    {
        public readonly string methodName;
        public ButtonAttribute(string methodName)
        {
            this.methodName = methodName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var button = attribute as ButtonAttribute;
            if (!GUI.Button(position, label)) return;

            Object target = property.serializedObject.targetObject;

            if (button?.methodName is null) return;

            MethodInfo method = target.GetType().GetMethod(button.methodName);
            method?.Invoke(target, null);
        }
    }
#endif
}
