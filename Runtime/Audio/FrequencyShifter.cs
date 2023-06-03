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
        [Range(20, 20480)] public float BaseFrequency;
        public PitchMultiplier PitchOffset;

        public FrequencyShifter(float baseFrequency)
        {
            BaseFrequency = baseFrequency;
            PitchOffset = new PitchMultiplier();
        }

        public float Value => PitchOffset.ApplyPitch(BaseFrequency);
        public static implicit operator FrequencyShifter(float baseFrequency) => new(baseFrequency);
        public static implicit operator float(FrequencyShifter frequencyShifter) => frequencyShifter.Value;
    }
}