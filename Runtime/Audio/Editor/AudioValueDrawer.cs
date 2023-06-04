using MaxVram.Extensions;
using UnityEditor;
using UnityEngine;

namespace MaxVram.Audio
{
    [CustomPropertyDrawer(typeof(AudioValue))]
    public class AudioValueDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int indent = EditorGUI.indentLevel;
            float lineHeight = EditorGUIUtility.singleLineHeight;

            SerializedProperty targetValue = property.FindPropertyRelative("TargetValue");
            SerializedProperty minValue = property.FindPropertyRelative("MinValue");
            SerializedProperty maxValue = property.FindPropertyRelative("MaxValue");
            SerializedProperty valueName = property.FindPropertyRelative("ValueName");
            SerializedProperty valueUnit = property.FindPropertyRelative("ValueUnit");
            SerializedProperty changed = property.FindPropertyRelative("Changed");
            
            var valueLabel = new GUIContent(valueName.stringValue);
            var unitLabel = new GUIContent(valueUnit.stringValue);
            
            var singleLine = new Rect(position) { height = lineHeight };
            label = EditorGUI.BeginProperty(singleLine, valueLabel, property);
            position = EditorGUI.PrefixLabel(singleLine, label);
            EditorGUI.indentLevel = 0;
            
            Rect sliderRect = position.GetRect( 6, 0, 4);
            Rect fieldRect = position.GetRect( 6, 4, 1);
            Rect unitRect = position.GetRect( 6, 5, 1);
            
            DrawSlider(sliderRect, targetValue, minValue, maxValue, changed);

            EditorGUI.BeginChangeCheck();
            float fieldValue = EditorGUI.FloatField(fieldRect, targetValue.floatValue);
            EditorGUI.LabelField(unitRect, unitLabel);
            
            if (EditorGUI.EndChangeCheck())
            {
                targetValue.floatValue = fieldValue;
                property.FindPropertyRelative("Changed").boolValue = true;
            }

            EditorGUI.indentLevel = indent;
        }
        
        protected virtual void DrawSlider(Rect sliderRect, SerializedProperty targetValue, SerializedProperty minValue, SerializedProperty maxValue, SerializedProperty changed)
        {
            EditorGUI.BeginChangeCheck();
            float sliderValue = GUI.HorizontalSlider(sliderRect, targetValue.floatValue, minValue.floatValue, maxValue.floatValue);

            if (EditorGUI.EndChangeCheck())
            {
                targetValue.floatValue = sliderValue;
                changed.boolValue = true;
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(AudioLevel))]
    public class AudioLevelDrawer : AudioValueDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
        }
    }
    
    [CustomPropertyDrawer(typeof(AudioFrequency))]
    public class AudioFrequencyDrawer : AudioValueDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
        }
        
        protected override void DrawSlider(Rect sliderRect, SerializedProperty targetValue, SerializedProperty minValue, SerializedProperty maxValue, SerializedProperty changed)
        {
            EditorGUI.BeginChangeCheck();
            float sliderValue = GUI.HorizontalSlider(sliderRect, MaxAudio.HertzToLinear(targetValue.floatValue), 0, 1);

            if (EditorGUI.EndChangeCheck())
            {
                targetValue.floatValue = Mathf.Clamp(MaxAudio.LinearToHertz(sliderValue), minValue.floatValue, maxValue.floatValue);
                changed.boolValue = true;
            }
        }
    }
}