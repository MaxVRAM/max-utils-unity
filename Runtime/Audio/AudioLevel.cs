using System;

namespace MaxVram.Audio
{
    /// <summary>
    /// AudioLevel is a wrapper for a float value to help manage the gain of an audio signal.
    /// </summary>
    [Serializable]
    public class AudioLevel : AudioValue
    {
        public float Loudness => MaxAudio.LinearToLoudness(CurrentValue);

        public AudioLevel(float value, float min = 0, float max = 1, bool smoothing = false) : base(value, min, max, smoothing)
        {
            ValueName = "Level";
            ValueUnit = "Gain";
        }
    }
}