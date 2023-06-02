using System;

namespace MaxVram.Audio
{
    [Serializable]
    public class Oscillator : SoundSource
    {
        public FrequencyShifter Frequency;
        public Waveforms Waveform;

        public Oscillator(Waveforms waveform, double frequency)
        {
            Frequency = new FrequencyShifter(frequency);
            Waveform = waveform;
        }

        public Oscillator()
        {
            Frequency = new FrequencyShifter(440);
            Waveform = Waveforms.Sine;
        }
        
        public void UpdateFrequency()
        {
            SetFrequency(Frequency);
        }
        
        public new float GetNextSample()
        {
            IncrementSample();
            return Waveform.GetSample(CurrentIndex);
        }
        
        // public void SetIncrement()
        // {
        //     if (OutputSampleRate == 0)
        //         return;
        //
        //     SetSourceSampleRate();
        //     double frequency = Mathf.Lerp((float)_currentFrequency, (float)Frequency, 0.2f);
        //     _currentFrequency = frequency;
        //     _increment = _currentFrequency * 2f * Mathf.PI / _sampleRate;
        // }


        // /// <summary>
        // /// Returns the current sample of the oscillator.
        // /// Desmos calculator graph for visual demonstration: https://www.desmos.com/calculator/m9v9lfwyyb
        // /// </summary>
        // public float Wave =>
        //     Waveform switch {
        //         Waveforms.Sine     => Mathf.Sin((float)_phase),
        //         Waveforms.Square   => Mathf.Sign(Mathf.Sin((float)_phase)),
        //         Waveforms.Sawtooth => 2f / Mathf.PI * ((float)_phase - Mathf.PI) / 2f,
        //         Waveforms.Triangle => 2f / Mathf.PI * Mathf.Asin(Mathf.Sin((float)_phase)),
        //         Waveforms.Noise    => 2f * Random.value - 1f,
        //         _                  => 0f
        //     };
    }
}