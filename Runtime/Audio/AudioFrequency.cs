using System;

namespace MaxVram.Audio
{
    /// <summary>
    /// AudioFrequency is a wrapper for a float value to help manage the frequency of a sound source.
    /// </summary>
    [Serializable]
    public class AudioFrequency : AudioValue
    {
        public AudioFrequency(float value, float min = 20, float max = 20480, bool smoothing = false) : base(value, min, max, smoothing)
        {
            ValueName = "Frequency";
            ValueUnit = "Hz";
        }
    }
}