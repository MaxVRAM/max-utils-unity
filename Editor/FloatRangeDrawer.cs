using UnityEditor;
using UnityEngine;

using MaxVram.Extensions;
using MaxVram.FancyGUI;

namespace MaxVram.CustomTypes
{
    [CustomPropertyDrawer(typeof(FloatRange))]
    public class FloatRangeDrawer : FancyPropertyDrawer
    {
        private int _numFields;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            label = EditorGUI.BeginProperty(position, label, property);
            
            SerializedProperty lowValue = property.FindPropertyRelative("LowValue");
            SerializedProperty highValue = property.FindPropertyRelative("HighValue");
            SerializedProperty rangeMode = property.FindPropertyRelative("RangeMode");

            float min = property.FindPropertyRelative("Min").floatValue;
            float max = property.FindPropertyRelative("Max").floatValue;

            const bool showResetButton = false;
            const int digits = 4;
            const float fieldWidth = 60;
            
            float xValue = lowValue.floatValue;
            float yValue = highValue.floatValue;

            position = EditorGUI.PrefixLabel(position, label);
            
            EditorGUI.indentLevel = 0;
            
            if (CheckResetButton(showResetButton, position))
            {
                rangeMode.boolValue = false;
                xValue = min;
                yValue = min;
            }
            
            string modeButtonText = rangeMode.boolValue ? "R" : "F";
            
            var buttonRect = new Rect(position) {
                x = position.x,
                width = ButtonSize,
                y = position.y + (position.height - ButtonSize) / 2,
                height = ButtonSize
            };
            
            if (GUI.Button(buttonRect, modeButtonText, ButtonStyle))
            {
                rangeMode.boolValue = !rangeMode.boolValue;
            }

            _numFields = rangeMode.boolValue ? 2 : 1;
            position.x += ButtonSize + Margin;
            position.width -= ButtonSize + Margin;
            
            float sliderWidth = position.width - _numFields * fieldWidth - _numFields * Margin;
            
            var fieldRect = new Rect(position) { width = fieldWidth };
            var sliderRect = new Rect(position) { x = fieldRect.xMax + Margin, width = sliderWidth };
            var secondFieldRect = new Rect(fieldRect) { x = sliderRect.xMax + Margin };
            
            EditorGUI.BeginChangeCheck();
            float fieldValueX = EditorGUI.FloatField(fieldRect, GUIContent.none, xValue.LimitDigits(digits));
            if (EditorGUI.EndChangeCheck())
                xValue = Mathf.Clamp(fieldValueX, min, max);
            
            if (rangeMode.boolValue)
            {
                EditorGUI.BeginChangeCheck();
                yValue = Mathf.Max(xValue, yValue);
                float fieldValueY = EditorGUI.FloatField(secondFieldRect, GUIContent.none, yValue.LimitDigits(digits));
                if (EditorGUI.EndChangeCheck())
                    yValue = Mathf.Clamp(fieldValueY, min, max);
            }

            if (rangeMode.boolValue)
                EditorGUI.MinMaxSlider(sliderRect, ref xValue, ref yValue, min, max);
            else
                xValue = GUI.HorizontalSlider(sliderRect, xValue, min, max);

            lowValue.floatValue = xValue;
            highValue.floatValue = yValue;
            EditorGUI.EndProperty();
            
            EditorGUI.indentLevel = indent;
        }
    }
}
