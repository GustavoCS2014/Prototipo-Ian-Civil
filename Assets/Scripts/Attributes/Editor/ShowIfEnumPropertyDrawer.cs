#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Attributes.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfEnumAttribute))]
    public class ShowIfEnumPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var showIfEnum = attribute as ShowIfEnumAttribute;
            bool enabled = GetConditionValue(showIfEnum, property);

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            if (enabled)
                EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = wasEnabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var showIf = attribute as ShowIfEnumAttribute;
            bool enabled = GetConditionValue(showIf, property);

            if (enabled)
                return EditorGUI.GetPropertyHeight(property, label);

            return -EditorGUIUtility.standardVerticalSpacing;
        }

        private static bool GetConditionValue(ShowIfEnumAttribute showIfEnum, SerializedProperty property)
        {
            SerializedProperty conditionProperty =
                property.serializedObject.FindProperty(showIfEnum.conditionalFieldName);
            return conditionProperty is not null && conditionProperty.enumValueIndex == showIfEnum.targetValue;
        }
    }
}
#endif