using UnityEditor;
using UnityEngine;

namespace Attributes
{
    public class ShowIfStringAttribute : PropertyAttribute
    {
        public readonly string conditionFieldName;
        public readonly string targetValue;

        public ShowIfStringAttribute(string conditionFieldName, string targetValue)
        {
            this.conditionFieldName = conditionFieldName;
            this.targetValue = targetValue;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowIfStringAttribute))]
    public class ShowIfStringPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var showIfString = attribute as ShowIfStringAttribute;
            bool enabled = GetConditionValue(showIfString, property);

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            if (enabled)
                EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = wasEnabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var showIf = attribute as ShowIfStringAttribute;
            bool enabled = GetConditionValue(showIf, property);

            if (enabled)
                return EditorGUI.GetPropertyHeight(property, label);

            return -EditorGUIUtility.standardVerticalSpacing;
        }

        private static bool GetConditionValue(ShowIfStringAttribute showIfString, SerializedProperty property)
        {
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIfString.conditionFieldName);
            return conditionProperty is not null && conditionProperty.stringValue == showIfString.targetValue;
        }
    }
#endif
}
