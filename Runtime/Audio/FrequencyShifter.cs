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
        public AudioFrequency BaseFrequency;
        public PitchMultiplier PitchOffset;

        public FrequencyShifter(float baseFrequency)
        {
            BaseFrequency = new AudioFrequency(baseFrequency);
            PitchOffset = new PitchMultiplier();
        }

        public float Value => PitchOffset.ApplyPitch(BaseFrequency.Value);
        public static implicit operator FrequencyShifter(float baseFrequency) => new(baseFrequency);
        public static implicit operator float(FrequencyShifter frequencyShifter) => frequencyShifter.Value;
    }
}