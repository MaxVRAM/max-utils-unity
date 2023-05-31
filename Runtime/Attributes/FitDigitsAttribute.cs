using UnityEngine;

namespace MaxVram.Attributes
{
    public class FitDigitsAttribute : PropertyAttribute
    {
        public readonly int Digits;
        public readonly float FieldWidth;
        
        public FitDigitsAttribute(int digits = 5, float fieldWidth = 50)
        {
            Digits = digits;
            FieldWidth = fieldWidth;
        }
    }
}
