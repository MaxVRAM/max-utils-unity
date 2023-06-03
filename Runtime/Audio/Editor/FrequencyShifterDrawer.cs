using System;
using UnityEditor;
using UnityEngine;

using MaxVram.Extensions;

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

            var freqLabel = new GUIContent("Hz", "Frequency");
            var singleLine = new Rect(position) { height = lineHeight };
            label = EditorGUI.BeginProperty(singleLine, label, property);
            position = EditorGUI.PrefixLabel(singleLine, label);
            EditorGUI.indentLevel = 0;

            Rect sliderRect = position.GetRect( 6, 0, 4);
            Rect fieldRect = position.GetRect( 6, 4, 1);
            Rect freqLabelRect = position.GetRect( 6, 5, 1);

            EditorGUI.BeginChangeCheck();
            float sliderHertz = GUI.HorizontalSlider(sliderRect, MaxAudio.HertzToLinear(freq.doubleValue), 0, 1);

            if (EditorGUI.EndChangeCheck())
                freq.doubleValue = Mathf.Clamp(MaxAudio.LinearToHertz(sliderHertz), 20f, 20480f);

            EditorGUI.BeginChangeCheck();
            double fieldHertz = EditorGUI.DoubleField(fieldRect, freq.doubleValue);
            EditorGUI.LabelField(freqLabelRect, freqLabel);

            EditorGUI.EndProperty();

            if (EditorGUI.EndChangeCheck())
                freq.doubleValue = Mathf.Clamp((float)fieldHertz, 20, 20480);

            var secondLine = new Rect(position) { y = position.y + lineHeight + 2 };
            EditorGUI.PropertyField(secondLine, pitch, GUIContent.none);
            
            EditorGUI.indentLevel = indent;
        }
    }
}