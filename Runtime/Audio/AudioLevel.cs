using System;
using UnityEngine;

namespace MaxVram.Audio
{
    /// <summary>
    /// AudioLevel is a wrapper for a float value to help manage the gain of an audio signal.
    /// </summary>
    [Serializable]
    public class AudioLevel
    {
        [SerializeField] [Range(0, 1)] private float _gain;
        private float _currentGain;
        private float _loudness;
        public float Loudness { get => _loudness; private set => _loudness = MaxAudio.LinearToLoudness(value); }
        private bool _requiresSmoothing;

        /// <summary>
        /// Creates a new AudioLevel instance with the specified starting gain.
        /// If a <c>requiresSmoothing</c> is true, gain will only be updated by manually calling the <c>Smooth</c> method.
        /// </summary>
        /// <param name="gain">The starting gain of the AudioLevel.</param>
        /// <param name="smoothing">Whether or not the AudioLevel should be smoothed.</param>
        public AudioLevel(float gain, bool smoothing = false)
        {
            _requiresSmoothing = smoothing;
            _gain = Mathf.Clamp01(gain);
            _loudness = MaxAudio.LinearToLoudness(_gain);
            _currentGain = 0;
        }

        public static implicit operator float(AudioLevel level) => level._gain;

        /// <summary>
        /// Updates the current gain value using a lerp to smooth the transition.
        /// </summary>
        /// <param name="deltaTime">Use delta time to define the smoothing rate. If providing an arbitrary value, recommended range is 0.01 and 0.1.</param>
        public void UpdateGain(float deltaTime)
        {
            _currentGain = Mathf.Lerp(_currentGain, _gain, deltaTime * 20);
            Loudness = _currentGain;
        }

        /// <summary>
        /// Sets the gain of the AudioLevel object, clamped between 0 and 1.
        /// If <c>requiresSmoothing</c> is true, gain will only be updated by manually calling the <c>Smooth</c> method.
        /// </summary>
        /// <param name="gain"></param>
        public void SetGain(float gain)
        {
            _gain = Mathf.Clamp01(gain);

            if (_requiresSmoothing)
                return;

            _currentGain = _gain;
            Loudness = _gain;
        }
    }
}