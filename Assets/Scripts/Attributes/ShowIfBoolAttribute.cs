using UnityEditor;
using UnityEngine;

namespace Attributes
{
    public class ShowIfBoolAttribute : PropertyAttribute
    {
        public readonly string conditionFieldName;

        public ShowIfBoolAttribute(string conditionFieldName)
        {
            this.conditionFieldName = conditionFieldName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowIfBoolAttribute))]
    public class ShowIfBoolPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var showIfBool = attribute as ShowIfBoolAttribute;
            bool enabled = GetConditionValue(showIfBool, property);

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            if (enabled)
                EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = wasEnabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var showIf = attribute as ShowIfBoolAttribute;
            bool enabled = GetConditionValue(showIf, property);

            if (enabled)
                return EditorGUI.GetPropertyHeight(property, label);

            return -EditorGUIUtility.standardVerticalSpacing;
        }

        private static bool GetConditionValue(ShowIfBoolAttribute showIfBool, SerializedProperty property)
        {
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIfBool.conditionFieldName);
            return conditionProperty is not null && conditionProperty.boolValue;
        }
    }
#endif
}
