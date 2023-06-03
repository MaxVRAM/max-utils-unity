using System;
using UnityEngine;

namespace MaxVram.Audio
{
    /// <summary>
    /// A basic oscillator to generate a waveform used to fill an AudioSource output buffer.
    /// </summary>
    [Serializable]
    public class Oscillator : SoundSource
    {
        public Waveforms Waveform;
        public FrequencyShifter Frequency;
        private float _currentFrequency;

        public Oscillator(Waveforms waveform, float frequency)
        {
            Frequency = new FrequencyShifter(frequency);
            Waveform = waveform;
        }

        public Oscillator()
        {
            Frequency = new FrequencyShifter(440);
            Waveform = Waveforms.Sine;
        }

        public override void Initialise(AudioConfiguration config, double sourceSampleCount = 0)
        {
            base.Initialise(config, 2 * Mathf.PI);
            _currentFrequency = Frequency;
        }

        /// <summary>
        /// Updates the current frequency value using a lerp to smooth the transition.
        /// </summary>
        /// <param name="deltaTime">Use delta time to define the smoothing rate. If providing an arbitrary value, recommended range is 0.01 and 0.1.</param>
        public void UpdateFrequency(float deltaTime)
        {
            if (!IsInitialised)
                return;
            
            _currentFrequency = Mathf.Lerp(_currentFrequency, Frequency, deltaTime * 20);
            SetIncrement();
        }

        public override float GetNextSample()
        {
            if (!IsInitialised)
                return 0;

            IncrementSample();
            return Waveform.GetSample(CurrentIndex);
        }

        protected override void SetIncrement()
        {
            if (!IsInitialised)
                return;

            Increment = _currentFrequency * PlaybackRatio;
        }

        protected override void IncrementSample()
        {
            if (!IsInitialised)
                return;

            CurrentIndex += Increment;
            if (CurrentIndex >= SourceSampleCount)
                CurrentIndex %= SourceSampleCount;
        }
    }
}