using UnityEditor;
using UnityEngine;

using MaxVram.Extensions;

namespace MaxVram.Attributes
{
    [CustomPropertyDrawer(typeof(FitDigitsAttribute))]
    public class FitDigitsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.Float)
            {
                EditorGUI.LabelField(position, label.text, "Use FitDigits with float.");
                return;
            }

            var floatDigits = (FitDigitsAttribute)attribute;
            int digits = floatDigits.Digits;
            float fieldWidth = floatDigits.FieldWidth;

            var fieldStyle = new GUIStyle(EditorStyles.numberField) { fixedWidth = fieldWidth };

            float fieldValue = property.floatValue;
            float digitsRoundedValue = fieldValue.LimitDigits(digits);

            EditorGUI.BeginChangeCheck();
            float newValue = EditorGUI.FloatField(position, label, digitsRoundedValue, fieldStyle);

            if (EditorGUI.EndChangeCheck())
            {
                property.floatValue = newValue;
            }

            EditorGUI.EndProperty();
        }
    }
}
