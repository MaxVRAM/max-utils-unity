using System;
using UnityEngine;

namespace MaxVram.Audio
{
    /// <summary>
    /// PitchShift offsets a base frequency (in hertz) using musical intervals; octave, semitone, and cent.
    /// </summary>
    [Serializable]
    public struct PitchShifter
    {
        [Range(-6, 6)] public int Octave;
        [Range(-12, 12)] public int Semitone;
        [Range(-100, 100)] public float Cent;

        public PitchShifter(int octave, int semitone, float cent)
        {
            Octave = octave;
            Semitone = semitone;
            Cent = cent;
        }

        public PitchShifter(int octave)
        {
            Octave = octave;
            Semitone = 0;
            Cent = 0;
        }

        /// <summary>
        /// Apply the pitch as an offset to a base frequency.
        /// </summary>
        /// <param name="baseFrequency">Arbitrary base frequency to offset the pitch of.</param>
        /// <returns>Musically adjusted frequency.</returns>
        public double ApplyPitch(double baseFrequency)
        {
            double pitchOffset = Mathf.Pow(2, Octave + Semitone / 12.0f + Cent / 1200.0f);
            double frequency = baseFrequency * pitchOffset;
            double result = frequency < MaxAudio.MinFrequency
                ? MaxAudio.MinFrequency
                : frequency > MaxAudio.MaxFrequency
                    ? MaxAudio.MaxFrequency
                    : frequency;

            return result;
        }
    }
}