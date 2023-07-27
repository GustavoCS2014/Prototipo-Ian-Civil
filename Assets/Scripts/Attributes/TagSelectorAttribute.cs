using UnityEditor;
using UnityEngine;

namespace Attributes
{
    public class TagSelectorAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType is SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);

                string tagValue = string.IsNullOrEmpty(property.stringValue) ? "Untagged" : property.stringValue;

                string tag = EditorGUI.TagField(position, label, tagValue);
                property.stringValue = tag;
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use <b>[TagSelector]</b> with string fields only.");
            }
        }
    }
#endif
}
