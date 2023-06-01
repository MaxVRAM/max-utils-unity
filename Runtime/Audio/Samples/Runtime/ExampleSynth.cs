using System.Collections;
using System.Collections.Generic;
using MaxVram.Audio;
using UnityEngine;

namespace MaxVram
{
    [RequireComponent(typeof(AudioSource))]
    public class ExampleSynth : MonoBehaviour
    {
        public Oscillator Voice;
        private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            Voice.SetSampleRate(AudioSettings.outputSampleRate);
        }
        
        private void Update()
        {
            Voice.UpdateIncrement();
        }
        
        private void OnAudioFilterRead(float[] data, int channels)
        {
            for (var i = 0; i < data.Length; i += channels)
            {
                data[i] = (float)Voice.NextSample();
                
                for (var j = 1; j < channels; j++)
                    data[i + j] = data[i];
            }
        }
    }
}
