using UnityEditor;
using UnityEngine;

namespace Attributes
{
    public class TagSelectorAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType is SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);
                string tag = EditorGUI.TagField(position, label, property.stringValue);
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
