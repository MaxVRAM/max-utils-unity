using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MaxVram.Audio
{
    [Serializable]
    public class Oscillator : SoundSource
    {
        [Range(0,2)] public double Gain;
        public FreqPitch Frequency;
        public Waveforms Waveform;
        private double _sampleRate;
        private double _increment;
        private double _phase;

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

            _increment = Frequency * 2.0 * Mathf.PI / _sampleRate;
        }

        public new double NextSample()
        {
            _phase += _increment;
            _phase %= 2.0 * Mathf.PI;
            return Wave * Gain;
        }

        public double Wave =>
            Waveform switch {
                Waveforms.Sine     => Mathf.Sin((float)_phase),
                Waveforms.Square   => Mathf.Sign(Mathf.Sin((float)_phase)),
                Waveforms.Sawtooth => 2.0 / Mathf.PI * (_phase - Mathf.PI / 2.0) * 0.5f,
                Waveforms.Triangle => 2.0 / Mathf.PI * Mathf.Asin(Mathf.Sin((float)_phase)),
                Waveforms.Noise    => 2.0 * Random.value - 1.0,
                _                  => 0.0
            };
    }
}