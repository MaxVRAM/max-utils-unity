using System;
using UnityEngine;

namespace MaxVram.Audio
{
    /// <summary>
    /// A frequency value with an exponentially spaced GUI slider and musical pitch offset.
    /// </summary>
    [Serializable]
    public struct FrequencyShifter
    {
        [Range(20, 20480)] public double BaseFrequency;
        public PitchShifter PitchOffset;

        public FrequencyShifter(double baseFrequency)
        {
            BaseFrequency = baseFrequency;
            PitchOffset = new PitchShifter(0);
        }

        public double Value => PitchOffset.ApplyPitch(BaseFrequency);
        public static implicit operator FrequencyShifter(double baseFrequency) => new(baseFrequency);
        public static implicit operator double(FrequencyShifter frequencyShifter) => frequencyShifter.Value;
        public static implicit operator float(FrequencyShifter frequencyShifter) => (float)frequencyShifter.Value;
    }
}