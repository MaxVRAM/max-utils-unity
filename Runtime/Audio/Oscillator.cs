using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MaxVram.Audio
{
    [Serializable]
    public class Oscillator : SoundSource
    {
        [Range(0,1)] public double Gain;
        public FreqPitch Frequency;
        public Waveforms Waveform;
        private double _sampleRate;
        private double _increment;
        private double _phase;
        private double _currentGain;
        private double _currentFrequency;

        public Oscillator(int sampleRate, Waveforms waveform, double frequency, double gain)
        {
            _sampleRate = sampleRate;
            Frequency = new FreqPitch(frequency);
            Waveform = waveform;
            Gain = gain;
        }

        public Oscillator()
        {
            _sampleRate = 44100;
            Frequency = new FreqPitch(440);
            Waveform = Waveforms.Sine;
            Gain = 1;
        }
        
        public void SetSampleRate(int sampleRate)
        {
            _sampleRate = sampleRate;
        }
        
        public void UpdateIncrement()
        {
            if (_sampleRate == 0)
                return;

            double frequency = Mathf.Lerp((float)_currentFrequency, (float)Frequency, 0.2f);
            _currentFrequency = frequency;
            _increment = _currentFrequency * 2f * Mathf.PI / _sampleRate;
        }

        public new double NextSample()
        {
            _phase += _increment;
            _phase %= 2f * Mathf.PI;
            double gain = Mathf.Lerp((float)_currentGain, (float)Gain, 0.2f);
            _currentGain = gain;
            return Wave * gain;
        }

        /// <summary>
        /// Returns the current sample of the oscillator.
        /// Desmos calculator graph for visual demonstration: https://www.desmos.com/calculator/m9v9lfwyyb
        /// </summary>
        public double Wave =>
            Waveform switch {
                Waveforms.Sine     => Mathf.Sin((float)_phase),
                Waveforms.Square   => Mathf.Sign(Mathf.Sin((float)_phase)),
                Waveforms.Sawtooth => 2f / Mathf.PI * (_phase - Mathf.PI) / 2f,
                Waveforms.Triangle => 2f / Mathf.PI * Mathf.Asin(Mathf.Sin((float)_phase)),
                Waveforms.Noise    => 2f * Random.value - 1f,
                _                  => 0f
            };
    }
}