using UnityEditor;
using UnityEngine;

namespace Attributes
{
    public class ShowIfStringAttribute : PropertyAttribute
    {
        public readonly string conditionalFieldName;
        public readonly string targetValue;

        public ShowIfStringAttribute(string conditionalFieldName, string targetValue)
        {
            this.conditionalFieldName = conditionalFieldName;
            this.targetValue = targetValue;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowIfStringAttribute))]
    public class ShowIfStringPropertyDrawer : PropertyDrawer
    {
        private ShowIfStringAttribute _showIfStringAttribute;
        private SerializedProperty _conditionalProperty;
        private string _targetValue;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _showIfStringAttribute ??= attribute as ShowIfStringAttribute;
            _conditionalProperty ??= property.serializedObject.FindProperty(_showIfStringAttribute!.conditionalFieldName);
            _targetValue ??= _showIfStringAttribute!.targetValue;

            if (_conditionalProperty.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, "ShowIfString supports only string properties.");
                return;
            }

            bool enabled = GetConditionValue();

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            if (enabled)
                EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = wasEnabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_conditionalProperty?.propertyType != SerializedPropertyType.String)
                return EditorGUI.GetPropertyHeight(property, label);

            if (GetConditionValue())
                return EditorGUI.GetPropertyHeight(property, label);

            return -EditorGUIUtility.standardVerticalSpacing;
        }

        private bool GetConditionValue()
        {
            return _conditionalProperty is not null && _conditionalProperty.stringValue == _targetValue;
        }
    }
#endif
}
