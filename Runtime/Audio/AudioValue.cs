using System;
using UnityEngine;

namespace MaxVram.Audio
{
    /// <summary>
    /// AudioValue is a wrapper for a float to help manage smoothing and clamping values.
    /// </summary>
    [Serializable]
    public class AudioValue
    {
        [SerializeField] protected float TargetValue;
        [SerializeField] protected float CurrentValue;
        [SerializeField] protected float MinValue;
        [SerializeField] protected float MaxValue;
        [SerializeField] protected bool RequiresSmoothing;
        [SerializeField] protected bool Changed;

        [SerializeField] protected string ValueName = "Value";
        [SerializeField] protected string ValueUnit = "Amount";
        
        public float SetMin(float value) => MinValue = value;
        public float SetMax(float value) => MaxValue = value;
        public bool SetSmoothing(bool value) => RequiresSmoothing = value;
        
        public float Value
        {
            get
            {
                if (Changed && !RequiresSmoothing)
                    CurrentValue = Mathf.Clamp(TargetValue, MinValue, MaxValue);
                
                Changed = false;
                return CurrentValue;
            }
            set
            {
                TargetValue = Mathf.Clamp(value, MinValue, MaxValue);
                Changed = true;
            } 
        }
        
        /// <summary>
        /// Creates a new AudioValue instance with the specified starting value.
        /// If a <c>requiresSmoothing</c> is true, the value will only be updated by manually calling the <c>Smooth</c> method.
        /// </summary>
        /// <param name="value">The starting value of the AudioValue object.</param>
        /// <param name="min">Minimum limit to clamp the value.</param>
        /// <param name="max">Maximum limit to clamp the value.</param>
        /// <param name="smoothing">Whether or not the resultant output should require calling the <c>Smooth</c> method to update.</param>
        public AudioValue(float value, float min = 0, float max = 1, bool smoothing = false)
        {
            MinValue = min;
            MaxValue = max;
            TargetValue = Mathf.Clamp(value, MinValue, MaxValue);
            CurrentValue = TargetValue;
            RequiresSmoothing = smoothing;
            Changed = true;
        }

        /// <summary>
        /// Updates the current value using lerp to smooth the transition.
        /// </summary>
        /// <param name="lerpValue">Recommended to use Time.deltaTime. If providing an arbitrary value, try between 0.01 and 0.05.</param>
        public virtual void Smooth(float lerpValue)
        {
            CurrentValue = Mathf.Lerp(CurrentValue, TargetValue, lerpValue);
        }
    }
}