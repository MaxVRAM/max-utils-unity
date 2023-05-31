using UnityEngine;

namespace MaxVram.Modules
{
    public static class MaxOscillate
    {
        public enum Waveform { Sine, Square, Sawtooth, Triangle, Noise }
        
        public class Oscillator
        {
            public Waveform Waveform;
            public double Frequency;
            public double Gain;
            
            private double _sampleRate;
            private double _increment;
            private double _phase;
            private int _octaveOffset;
            private int _semitoneOffset;
            private float _centOffset;
            
            public Oscillator(double sampleRate, Waveform waveform, double frequency, double gain)
            {
                _sampleRate = sampleRate;
                Waveform = waveform;
                Frequency = frequency;
                Gain = gain;
            }
            
            // TODO: Make function for returning frequency adjusted for octave, semitone, and cent offsets

            public void Increment()
            {
                _increment = Frequency * 2.0 * Mathf.PI / _sampleRate;
            }

            public double NextSample() 
            {
                _phase += _increment;
                _phase %= 2.0 * Mathf.PI;
                return Wave * Gain;
            }

            public double Wave => Waveform switch
            {
                Waveform.Sine     => Gain * Mathf.Sin((float)_phase),
                Waveform.Square   => Gain * Mathf.Sign(Mathf.Sin((float)_phase)),
                Waveform.Sawtooth => Gain * (2.0 / Mathf.PI) * (_phase - Mathf.PI / 2.0),
                Waveform.Triangle => Gain * (2.0 / Mathf.PI) * Mathf.Asin(Mathf.Sin((float)_phase)),
                Waveform.Noise    => Gain * (2.0 * Random.value - 1.0),
                _                 => 0.0
            }; 
        }
    }
}