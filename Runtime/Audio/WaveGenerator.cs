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
        public static float Sine(float index) => Mathf.Sin(index);

        public static float Square(float index) => Mathf.Sign(Mathf.Sin(index));

        public static float Sawtooth(float index) => 2f / Mathf.PI * (index - Mathf.PI) / 2f;

        public static float Triangle(float index) => 2f / Mathf.PI * Mathf.Asin(Mathf.Sin(index));
        
        public static float GetSample(this Waveforms waveform, float index) =>
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