using System;
using UnityEngine;

namespace MaxVram.Audio
{
    /// <summary>
    /// A frequency value with an exponentially spaced GUI slider and musical pitch offset.
    /// </summary>
    [Serializable]
    public struct FreqPitch
    {
        [Range(20,20480)] public double Hertz;
        public PitchShift Pitch;
        
        public FreqPitch(double hertz)
        {
            Hertz = hertz;
            Pitch = new PitchShift(0);
        }
        
        public double Value => Pitch.Apply(Hertz);
        
        // public float NormalisedExponentialValue => ExponentialSlider(20, 20480);
        // public float ExponentialSlider(float min, float max) => Mathf.Log((float)Hertz / min) / Mathf.Log(max / min);
        
        public static implicit operator FreqPitch(double hertz) => new(hertz);
        public static implicit operator double(FreqPitch freqPitch) => freqPitch.Value;
    }
}