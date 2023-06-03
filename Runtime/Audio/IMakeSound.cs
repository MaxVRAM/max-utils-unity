using UnityEngine;

namespace MaxVram.Audio
{
    public interface IMakeSound
    {
        public void Initialise(AudioConfiguration config, double sourceSampleCount);

        public float GetNextSample();

        public float GetSampleAt(float index);
    }
}