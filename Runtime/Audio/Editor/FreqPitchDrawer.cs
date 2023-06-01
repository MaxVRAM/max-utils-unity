using System;
using UnityEditor;
using UnityEngine;

namespace MaxVram.Audio
{
    [CustomPropertyDrawer(typeof(FreqPitch))]
    public class FreqPitchDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + 6;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            float lineHeight = EditorGUIUtility.singleLineHeight;
            label = EditorGUI.BeginProperty(position, label, property);

            SerializedProperty hertz = property.FindPropertyRelative("Hertz");
            SerializedProperty pitch = property.FindPropertyRelative("Pitch");
            SerializedProperty pitchOct = pitch.FindPropertyRelative("Octave");
            SerializedProperty pitchSemi = pitch.FindPropertyRelative("Semitone");
            SerializedProperty pitchCent = pitch.FindPropertyRelative("Cent");

            var hertzLabel = new GUIContent("Hz", "Frequency");
            var octaveLabel = new GUIContent("Oct", "Octave");
            var semitoneLabel = new GUIContent("Semi", "Semitone");
            var centLabel = new GUIContent("Cent", "Percentage");

            position = EditorGUI.PrefixLabel(position, label);
            EditorGUI.indentLevel = 0;

            Rect OffsetRect(Rect rect, int index) => new(rect) { x = rect.x + rect.width * index };

            var pitchFieldRect = new Rect(position) {
                x = position.x + 2, width = position.width / 6, height = lineHeight, y = position.y + lineHeight + 2
            };

            var fieldRect = new Rect(OffsetRect(pitchFieldRect, 4)) { y = position.y };
            var freqLabelRect = new Rect(OffsetRect(pitchFieldRect, 5)) { y = position.y };
            var sliderRect = new Rect(position) { xMax = fieldRect.x - 2, height = lineHeight };

            EditorGUI.BeginChangeCheck();
            float sliderHertz = GUI.HorizontalSlider(sliderRect, MaxAudio.HertzToLinear(hertz.doubleValue), 0, 1);

            if (EditorGUI.EndChangeCheck())
                hertz.doubleValue = Mathf.Clamp(MaxAudio.LinearToHertz(sliderHertz), 20f, 20480f);

            EditorGUI.LabelField(freqLabelRect, hertzLabel);
            
            EditorGUI.EndProperty();

            EditorGUI.BeginChangeCheck();
            double fieldHertz = EditorGUI.DoubleField(fieldRect, hertz.doubleValue);

            if (EditorGUI.EndChangeCheck())
                hertz.doubleValue = Mathf.Clamp((float)fieldHertz, 20, 20480);
            
            pitchOct.intValue = Math.Clamp(EditorGUI.IntField(pitchFieldRect, pitchOct.intValue), -10, 10);
            EditorGUI.LabelField(OffsetRect(pitchFieldRect, 1), octaveLabel);
            pitchSemi.intValue = Math.Clamp(EditorGUI.IntField(OffsetRect(pitchFieldRect, 2), pitchSemi.intValue), -12, 12);
            EditorGUI.LabelField(OffsetRect(pitchFieldRect, 3), semitoneLabel);
            pitchCent.floatValue = Mathf.Clamp(EditorGUI.FloatField(OffsetRect(pitchFieldRect, 4), pitchCent.floatValue), -100, 100);
            EditorGUI.LabelField(OffsetRect(pitchFieldRect, 5), centLabel);

            EditorGUI.indentLevel = indent;
        }
    }
}