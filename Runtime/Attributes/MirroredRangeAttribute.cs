using UnityEngine;

namespace MaxVram.Attributes
{
    public class MirroredRangeAttribute : PropertyAttribute
    {
        public readonly float Min;
        public readonly float Max;
        public readonly bool ShowResetButton;
        public readonly int Digits;
        public readonly float FieldWidth;

        public MirroredRangeAttribute(float min, float max, bool showResetButton = true, int digits = 5, float fieldWidth = 50)
        {
            Min = min;
            Max = max;
            ShowResetButton = showResetButton;
            Digits = digits;
            FieldWidth = fieldWidth;
        }
    }
}
