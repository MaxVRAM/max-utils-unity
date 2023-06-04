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
        public AudioLevel Level = new(0.5f);
        public Oscillator Voice = new(Waveforms.Sine, 440);

        private void Awake()
        {
            Debug.Log("ExampleSynth Awake");
            Voice.Initialise(AudioSettings.GetConfiguration());
        }

        private void Update()
        {
            //Level.UpdateGain(Time.deltaTime);
            Voice.UpdateFrequency(Time.deltaTime);
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            Voice.FillBuffer(ref data, channels, Level.Value);
        }
    }
}