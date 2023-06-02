using UnityEngine;

namespace MaxVram.Audio
{
    /// <summary>
    /// Low-level sound source interface.
    /// </summary>
    public class SoundSource : IMakeSound
    {
        protected int OutputSampleRate { get; private set; }
        protected double OutputBufferSize { get; private set; }
        protected double SourceSampleRate { get; private set; }
        protected double SampleRateRatio { get; private set; }
        protected int SourceSampleCount { get; private set; }
        protected int SourceChannels { get; private set; }
        protected int CurrentIndex { get; set; }

        protected SoundSource() { }

        public void Initialise(AudioConfiguration config)
        {
            OutputSampleRate = config.sampleRate;
            OutputBufferSize = config.dspBufferSize;
            SourceSampleCount = OutputSampleRate;
        }

        public void DefineSourceConfig(double sampleRate, int sampleCount, int channels)
        {
            SourceSampleRate = sampleRate;
            SourceSampleCount = sampleCount;
            SourceChannels = channels;
            SampleRateRatio = SourceSampleRate / OutputSampleRate;
        }
        
        public void SetSourceSampleRate(double sampleRate)
        {
            SourceSampleRate = sampleRate;
            SampleRateRatio = SourceSampleRate / OutputSampleRate;
        }
        
        public void SetFrequency(float frequency)
        {
            SourceSampleRate = frequency;
            SampleRateRatio = SourceSampleRate / OutputSampleRate;
        }
        
        public void SetPlaybackRate(float playbackRate)
        {
            SampleRateRatio = playbackRate;
        }

        protected void IncrementSample()
        {
            CurrentIndex += (int)(SampleRateRatio / SourceSampleCount);
            if (CurrentIndex >= SourceSampleCount)
                CurrentIndex %= SourceSampleCount;
        }

        public virtual float GetNextSample()
        {
            return 0;
        }

        public virtual float GetSampleAt(double index)
        {
            return 0;
        }

        public void FillBuffer(ref float[] buffer, int channels, float gain = 1f)
        {
            for (var i = 0; i < buffer.Length; i += channels)
            {
                float sample = GetNextSample() * gain;

                for (var j = 0; j < channels; j++)
                    buffer[i + j] = sample;
            }
        }
    }
}