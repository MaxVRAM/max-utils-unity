using System;
using Random = UnityEngine.Random;

namespace MaxVram.CustomTypes
{
    [Serializable]
    public struct FloatRange
    {
        public float LowValue;
        public float HighValue;
        public bool RangeMode;
        public float Min;
        public float Max;
        
        public float Value => RangeMode ? Random.Range(LowValue, HighValue) : LowValue;

        /// <summary>
        /// Initialise FloatRange in FLOAT mode, with Value calls returning the min value.
        /// </summary>
        public FloatRange(float min, float max, float defaultValue)
        {
            LowValue = defaultValue;
            HighValue = defaultValue;
            RangeMode = false;
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Initialise FloatRange in RANGE mode, with Value calls returning a value between the supplied min and max.
        /// </summary>
        public FloatRange(float min, float max)
        {
            LowValue = min;
            HighValue = max;
            RangeMode = true;
            Min = min;
            Max = max;
        }
    }
}
