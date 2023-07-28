using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace Attributes.Editor
{
    [CustomPropertyDrawer(typeof(CallerAttribute))]
    public class CallerPropertyDrawer : PropertyDrawer
    {
        private string[] methods;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, "Caller supports only string properties.");
                return;
            }

            var caller = attribute as CallerAttribute;

            Type type = caller!.scriptType;
            var target = property.serializedObject.targetObject;
            methods ??= GetScriptMethods(target, type);

            int newIndex = EditorGUI.Popup(position, label.text, Array.IndexOf(methods, property.stringValue), methods);

            if (newIndex >= 0)
                property.stringValue = methods[newIndex];

            if (methods.Length > 0 && GUILayout.Button(property.stringValue))
            {
                CallMethod(target, property.stringValue);
            }
        }

        private static string[] GetScriptMethods(object target, Type type)
        {
            return target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.DeclaringType == type)
                .Select(m => m.Name)
                .ToArray();
        }

        private static void CallMethod(object target, string methodName)
        {
            var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            method?.Invoke(target, null);
        }
    }
}
#endif
