using UnityEditor;
using UnityEngine;

namespace MaxVram.Audio
{
    [CustomPropertyDrawer(typeof(FrequencyShifter))]
    public class FrequencyShifterDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 6;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            float lineHeight = EditorGUIUtility.singleLineHeight;

            SerializedProperty freq = property.FindPropertyRelative("BaseFrequency");
            SerializedProperty pitch = property.FindPropertyRelative("PitchOffset");

            var singleLine = new Rect(position) { height = lineHeight };
            var secondLine = new Rect(singleLine) { y = position.y + lineHeight + 2 };

            EditorGUI.PropertyField(singleLine, freq);
            EditorGUI.PropertyField(secondLine, pitch);
            
            EditorGUI.indentLevel = indent;
        }
    }
}