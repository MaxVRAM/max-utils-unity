using System;
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
            SerializedProperty octField = pitch.FindPropertyRelative("Octave");
            SerializedProperty semiField = pitch.FindPropertyRelative("Semitone");
            SerializedProperty centField = pitch.FindPropertyRelative("Cent");

            var freqLabel = new GUIContent("Hz", "Frequency");
            var octLabel = new GUIContent("Oct", "Octave");
            var semiLabel = new GUIContent("Semi", "Semitone");
            var centLabel = new GUIContent("Cent", "Percentage");

            var singleLine = new Rect(position) { height = lineHeight };
            label = EditorGUI.BeginProperty(singleLine, label, property);
            position = EditorGUI.PrefixLabel(singleLine, label);
            EditorGUI.indentLevel = 0;

            Rect sliderRect = GetRect(position, 6, 0, 4);
            Rect fieldRect = GetRect(position, 6, 4, 1);
            Rect freqLabelRect = GetRect(position, 6, 5, 1);

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
            Rect[] rects = GetEqualRectArray(secondLine, 6);

            octField.intValue = Math.Clamp(EditorGUI.IntField(rects[0], octField.intValue), -10, 10);
            EditorGUI.LabelField(rects[1], octLabel);

            semiField.intValue = Math.Clamp(EditorGUI.IntField(rects[2], semiField.intValue), -12, 12);
            EditorGUI.LabelField(rects[3], semiLabel);

            centField.floatValue = Mathf.Clamp(EditorGUI.FloatField(rects[4], centField.floatValue), -100, 100);
            EditorGUI.LabelField(rects[5], centLabel);

            EditorGUI.indentLevel = indent;
        }

        private static Rect[] GetEqualRectArray(Rect position, int divisions)
        {
            float divWidth = position.width / divisions;
            var rects = new Rect[divisions];

            for (var i = 0; i < divisions; i++)
                rects[i] = new Rect(position.x + divWidth * i, position.y, divWidth - 2, position.height);

            return rects;
        }

        private static Rect CombineRects(Rect[] rects)
        {
            var rect = new Rect(rects[0]);
            float xMin = rect.xMin;
            float xMax = rect.xMax;

            foreach (Rect r in rects)
            {
                xMin = xMin < r.xMin ? xMin : r.xMin;
                xMax = xMax > r.xMax ? xMax : r.xMax;
            }

            return rect;
        }

        private static Rect GetRect(Rect position, int divisions, int startIndex, int width)
        {
            Rect returnRect = position;
            float divWidth = returnRect.width / divisions;
            returnRect = new Rect(returnRect.x + divWidth * startIndex, returnRect.y, divWidth * width - 2, returnRect.height);
            return returnRect;
        }
    }
}