using UnityEngine;

namespace MaxVram.Audio
{
    public interface IMakeSound
    {
        public void Initialise(AudioConfiguration config);
        
        public float GetNextSample();
        
        public float GetSampleAt(double index);
    }
}