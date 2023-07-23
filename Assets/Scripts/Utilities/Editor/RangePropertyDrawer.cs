using UnityEditor;
using UnityEngine;

namespace Utilities.Editor
{
    [CustomPropertyDrawer(typeof(Range))]
    public class RangePropertyDrawer : PropertyDrawer
    {
        private const float SubLabelSpacing = 4;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            label = EditorGUI.BeginProperty(pos, label, prop);
            Rect contentRect = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
            GUIContent[] labels = { new GUIContent("Min"), new GUIContent("Max") };
            SerializedProperty[] properties = { prop.FindPropertyRelative("min"), prop.FindPropertyRelative("max") };
            DrawMultiplePropertyFields(contentRect, labels, properties);

            EditorGUI.EndProperty();
        }

        private static void DrawMultiplePropertyFields(Rect pos, GUIContent[] subLabels, SerializedProperty[] props)
        {
            // backup gui settings
            int originalIndentLevel = EditorGUI.indentLevel;
            float originalLabelWidth = EditorGUIUtility.labelWidth;

            // draw properties
            int propsCount = props.Length;
            float width = (pos.width - (propsCount - 1) * SubLabelSpacing) / propsCount;
            Rect contentPos = new(pos.x, pos.y, width, pos.height);
            EditorGUI.indentLevel = 0;
            for (int i = 0; i < propsCount; i++)
            {
                EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(subLabels[i]).x;
                EditorGUI.PropertyField(contentPos, props[i], subLabels[i]);
                contentPos.x += width + SubLabelSpacing;
            }

            EditorGUIUtility.labelWidth = originalLabelWidth;
            EditorGUI.indentLevel = originalIndentLevel;
        }
    }
}
