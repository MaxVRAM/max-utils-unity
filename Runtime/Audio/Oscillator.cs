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
        public FrequencyShifter FrequencyShift;

        public Oscillator(Waveforms waveform, float frequency)
        {
            FrequencyShift = new FrequencyShifter(frequency);
            Waveform = waveform;
        }

        public Oscillator()
        {
            FrequencyShift = new FrequencyShifter(440);
            Waveform = Waveforms.Sine;
        }

        public override void Initialise(AudioConfiguration config, double sourceSampleCount = 0)
        {
            base.Initialise(config, 2 * Mathf.PI);
        }

        /// <summary>
        /// Updates the current frequency value using a lerp to smooth the transition.
        /// </summary>
        /// <param name="deltaTime">Use delta time to define the smoothing rate. If providing an arbitrary value, recommended range is 0.01 and 0.1.</param>
        public void UpdateFrequency(float deltaTime)
        {
            if (!IsInitialised)
                return;
            
            FrequencyShift.BaseFrequency.Smooth(deltaTime);
            SetIncrement();
        }

        public override float GetNextSample()
        {
            if (!IsInitialised)
                return 0;

            IncrementIndex();
            return Waveform.SampleWaveform(CurrentIndex);
        }

        protected override void SetIncrement()
        {
            if (!IsInitialised)
                return;

            Increment = FrequencyShift.Value * PlaybackRatio;
        }

        protected override void IncrementIndex()
        {
            if (!IsInitialised)
                return;

            CurrentIndex += Increment;
            if (CurrentIndex >= SourceSampleCount)
                CurrentIndex %= SourceSampleCount;
        }
    }
}