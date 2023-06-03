using System;
using UnityEngine;

namespace MaxVram.Audio
{
    /// <summary>
    /// Multiplies a base frequency (in hertz) using musical intervals; octave, semitone, and cent.
    /// </summary>
    [Serializable]
    public class PitchMultiplier
    {
        [SerializeField] [Range(-6, 6)] private int _octave;
        [SerializeField] [Range(-12, 12)] private int _semitone;
        [SerializeField] [Range(-100, 100)] private float _cent;
        private bool _changed;
        private float _pitch;
        public float Pitch
        {
            get
            {
                if (!_changed && _pitch != 0)
                    return _pitch;

                _pitch = Mathf.Pow(2, _octave + _semitone / 12f + _cent / 1200f);
                _changed = false;
                return _pitch;
            }
            private set => _pitch = value;
        }

        public int Octave
        {
            get => _octave;
            set
            {
                _octave = Math.Clamp(value, -6, 6);
                _changed = true;
            }
        }
        public int Semitone
        {
            get => _semitone;
            set
            {
                _semitone = Math.Clamp(value, -12, 12);
                _changed = true;
            }
        }
        public float Cent
        {
            get => _cent;
            set
            {
                _cent = Mathf.Clamp(value, -100, 100);
                _changed = true;
            }
        }

        public PitchMultiplier(int octave, int semitone, float cent)
        {
            _octave = octave;
            _semitone = semitone;
            _cent = cent;
            _pitch = 0;
            _changed = true;
        }

        public PitchMultiplier()
        {
            _octave = 0;
            _semitone = 0;
            _cent = 0;
            _pitch = 0;
            _changed = true;
        }

        /// <summary>
        /// Apply pitch as a multiplier to a base frequency.
        /// </summary>
        /// <param name="baseFrequency">Arbitrary base frequency to apply pitch offset to.</param>
        /// <returns>Musically adjusted frequency.</returns>
        public float ApplyPitch(float baseFrequency)
        {
            float frequency = baseFrequency * Pitch;
            return Mathf.Clamp(frequency, MaxAudio.MinFrequency, MaxAudio.MaxFrequency);
        }
    }
}