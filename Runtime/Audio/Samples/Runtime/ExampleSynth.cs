using UnityEngine;
using MaxVram.Audio;

namespace MaxVram
{
    /// <summary>
    /// Basic oscillator synth example.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class ExampleSynth : MonoBehaviour
    {
        public AudioLevel Level = new(0.5f, true);
        public Oscillator Voice;
        
        private void Awake()
        {
            Voice.Initialise(AudioSettings.GetConfiguration());
        }
        
        private void Update()
        {
            Level.SmoothGain(Time.deltaTime);
            Voice.UpdateFrequency();
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            Voice.FillBuffer(ref data, channels, Level.Loudness);
        }
    }
}