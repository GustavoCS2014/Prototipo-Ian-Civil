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
        private const BindingFlags Filter = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private const float ButtonWidth = 50f;
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

            if (methods?.Length == 0)
            {
                EditorGUI.LabelField(position, $"No methods found in {type.Name}.");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            Rect contentRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var popupRect = new Rect(contentRect.x, contentRect.y, contentRect.width - ButtonWidth, contentRect.height);
            var buttonRect = new Rect(contentRect.x + contentRect.width - ButtonWidth, contentRect.y, ButtonWidth, contentRect.height);

            int i = EditorGUI.Popup(popupRect, Array.IndexOf(methods, property.stringValue), methods);

            property.stringValue = methods[i];

            if (GUI.Button(buttonRect, "Call"))
            {
                CallMethod(target, methods[i]);
            }

            EditorGUI.EndProperty();
        }

        private static string[] GetScriptMethods(object target, Type type)
        {
            return target.GetType()
                .GetMethods(Filter)
                .Where(m => m.DeclaringType == type)
                .Select(m => m.Name)
                .ToArray();
        }

        private static void CallMethod(object target, string methodName)
        {
            target.GetType().GetMethod(methodName, Filter)!.Invoke(target, null);
        }
    }
}
#endif
