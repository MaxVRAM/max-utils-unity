using UnityEngine;

namespace MaxVram.Audio
{
    public static class MaxAudio
    {
        public const int MinFrequency = 20;
        public const int MaxFrequency = 20480;
        
        /// <summary>
        /// Exponentially processes a normalised linear input value to replicate the human perception of loudness.
        /// Using this to set the volume of two identical AudioSources, one fading up and the other down, will perform
        /// a cross-fade with a perceived equal power curve. Can also be used for helping to represent pitch.
        /// </summary>
        public static float LinearToLoudness(float linear)
        {
            return linear switch {
                <= 0.005f => 0,
                >= 0.995f => 1,
                _         => (Mathf.Log10(linear + 0.1f) + 1f) * 0.9602f
            };
        }

        /// <summary>
        /// Converts a normalised (0-1) linear value to a logarithmic value in Hertz (20 to 20,480 Hz).
        /// </summary>
        public static float LinearToHertz (float value) => 20 * Mathf.Pow(2, 10 * value);
        
        /// <summary>
        /// Converts a logarithmic value in Hertz (20 to 20,480 Hz) to a normalised (0-1) linear value.
        /// </summary>
        public static float HertzToLinear (double value) => Mathf.Log((float) value / 20, 2) / 10;
        
        public static float BufferGetSample(float[] dspBuffer, int writeIndex, float readIndex)
        {
            float localIndex = writeIndex - readIndex;

            while (localIndex >= dspBuffer.Length)
                localIndex -= dspBuffer.Length;

            while (localIndex < 0)
                localIndex += dspBuffer.Length;

            return LinearInterpolate(dspBuffer, localIndex);
        }

        public static void BufferAddSample(float[] dspBuffer, ref int writeIndex, float sampleValue)
        {
            writeIndex %= dspBuffer.Length;
            dspBuffer[writeIndex] = sampleValue;
            writeIndex++;
        }

        public static float SineOscillator(ref float phase, float freq, int sampleRate)
        {
            float inc = freq * 2 * Mathf.PI / sampleRate;
            float value = Mathf.Sin(phase);

            phase += inc;

            while (phase >= Mathf.PI * 2)
                phase -= Mathf.PI * 2;

            return value;
        }

        public static float LinearInterpolate(float[] dspBuffer, float index)
        {
            var lower = (int)index;
            int upper = lower + 1;

            if (upper == dspBuffer.Length)
                upper = 0;

            float difference = index - lower;

            return dspBuffer[upper] * difference + dspBuffer[lower] * (1 - difference);
        }

        public static float UnbiasedInput(float inputSample, float previousInput, float previousOutput)
        {
            return inputSample - previousInput + previousOutput * 0.99997f;
        }

        public static bool IsCrossing(float inputSample, float previousSample)
        {
            return inputSample > 0 && previousSample <= 0;
        }
    }
}
