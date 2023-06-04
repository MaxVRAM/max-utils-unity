using System;
using UnityEditor;
using UnityEngine;

using MaxVram.Extensions;

namespace MaxVram.Audio
{
    [CustomPropertyDrawer(typeof(PitchMultiplier))]
    public class PitchMultiplierDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            float lineHeight = EditorGUIUtility.singleLineHeight;

            SerializedProperty octField = property.FindPropertyRelative("_octave");
            SerializedProperty semiField = property.FindPropertyRelative("_semitone");
            SerializedProperty centField = property.FindPropertyRelative("_cent");
            
            var octLabel = new GUIContent("Oct", "Octave");
            var semiLabel = new GUIContent("Semi", "Semitone");
            var centLabel = new GUIContent("Cent", "Percentage");
            
            var singleLine = new Rect(position) { height = lineHeight };
            label = EditorGUI.BeginProperty(singleLine, label, property);
            position = EditorGUI.PrefixLabel(singleLine, label);
            EditorGUI.indentLevel = 0;
            
            Rect[] rects = position.Divide(6);

            EditorGUI.BeginChangeCheck();
            
            octField.intValue = Math.Clamp(EditorGUI.IntField(rects[0], octField.intValue), -10, 10);
            EditorGUI.LabelField(rects[1], octLabel);

            semiField.intValue = Math.Clamp(EditorGUI.IntField(rects[2], semiField.intValue), -12, 12);
            EditorGUI.LabelField(rects[3], semiLabel);

            centField.floatValue = Mathf.Clamp(EditorGUI.FloatField(rects[4], centField.floatValue), -100, 100);
            EditorGUI.LabelField(rects[5], centLabel);
            
            if (EditorGUI.EndChangeCheck())
            {
                SerializedProperty changed = property.FindPropertyRelative("Changed");
                changed.boolValue = true;
            }

            EditorGUI.indentLevel = indent;
        }
    }
}