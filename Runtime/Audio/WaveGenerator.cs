using UnityEngine;

namespace MaxVram.Audio
{
    public enum Waveforms { Sine, Square, Sawtooth, Triangle, Noise }

    /// <summary>
    /// Returns a sample at the given index from a generated waveform.
    /// Desmos calculator graph for visual demonstration: https://www.desmos.com/calculator/m9v9lfwyyb
    /// </summary>
    public static class WaveGenerator
    {
        public static float Sine(double index) => Mathf.Sin((float)index);

        public static float Square(double index) => Mathf.Sign(Mathf.Sin((float)index));

        public static float Sawtooth(double index) => 2f / Mathf.PI * ((float)index - Mathf.PI) / 2f;

        public static float Triangle(double index) => 2f / Mathf.PI * Mathf.Asin(Mathf.Sin((float)index));
        
        public static float SampleWaveform(this Waveforms waveform, double index) =>
            waveform switch {
                Waveforms.Sine     => Sine(index),
                Waveforms.Square   => Square(index),
                Waveforms.Sawtooth => Sawtooth(index),
                Waveforms.Triangle => Triangle(index),
                Waveforms.Noise    => 2f * Random.value - 1f,
                _                  => 0f
            };
    }
}