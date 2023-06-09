using UnityEngine;

namespace MaxVram.Modules
{
    public struct MaxMath
    {
        public static float OneDegreeFraction => 0.0027777777f;

        public static float Map(float val, float inMin, float inMax, float outMin, float outMax)
        {
            return (val - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        }

        public static float Map(float val, Vector2 inRange, float outMin, float outMax)
        {
            return Map(val, inRange.x, inRange.y, outMin, outMax);
        }

        public static float Map(float val, Vector2 inRange, Vector2 outRange)
        {
            return Map(val, inRange.x, inRange.y, outRange.x, outRange.y);
        }

        public static float Map(float val, float inMin, float inMax, float outMin, float outMax, float exp)
        {
            return Mathf.Pow((val - inMin) / (inMax - inMin), exp) * (outMax - outMin) + outMin;
        }

        public static float Smooth(float currentValue, float targetValue, float smoothing, float epsilon = 0.001f)
        {
            epsilon = epsilon <= Mathf.Epsilon ? Mathf.Epsilon : epsilon;

            if (smoothing > epsilon && Mathf.Abs(currentValue - targetValue) > epsilon)
                return Mathf.Lerp(currentValue, targetValue, (1 - smoothing) * 10f * Time.deltaTime);

            return targetValue;
        }
        
        /// <summary>
        /// Returns the centre of a range between two floats.
        /// </summary>
        public static float RangeCentre(float min, float max)
        {
            float centrePoint = (max - min) / 2;
            return min + centrePoint;
        }
        
        public static float InverseLerp(float a , float b, float value, bool absolute = false)
        {
            float scaledValue = Map(value, a, b, 0, 1);
            scaledValue = absolute ? Mathf.Abs(scaledValue) : scaledValue;
            return Mathf.Clamp01(scaledValue);
        }
        
        /// <summary>
        /// Linearly interpolate two components of a normalised Vector2 between min and max range scalars.
        /// </summary>
        /// <param name="min">The start values.</param>
        /// <param name="max">The end values.</param>
        /// <param name="normalisedVector">The interpolation position for the corresponding component of vector a and b.</param>
        /// <returns>A ranged Vector2.</returns>
        public static Vector2 NormToRanged(float min, float max, Vector2 normalisedVector)
        {
            return new Vector2(
                Mathf.Lerp(min, max, normalisedVector.x), 
                Mathf.Lerp(min, max, normalisedVector.y));
        }
        
        public static Vector2 NormToRanged(Vector2 minMaxVector, Vector2 normalisedVector)
        {
            return NormToRanged(minMaxVector.x, minMaxVector.y, normalisedVector);
        }
        
        /// <summary>
        /// Normalise each ranged component of a Vector2 that sit within min and max range scalars.
        /// </summary>
        /// <param name="min">The start of the range.</param>
        /// <param name="max">The end of the range.</param>
        /// <param name="rangedVector">The points within the two ranges that will be independently normalised.</param>
        /// <returns>A normalised Vector2.</returns>
        public static Vector2 RangedToNorm(float min, float max, Vector2 rangedVector)
        {
            return new Vector2(
                Mathf.InverseLerp(min, max, rangedVector.x), 
                Mathf.InverseLerp(min, max, rangedVector.y));
        }
        
        public static Vector2 RangedToNorm(Vector2 minMaxVector, Vector2 rangedVector)
        {
            return RangedToNorm(minMaxVector.x, minMaxVector.y, rangedVector);
        }
        
        public static float LargestComponent(Vector3 vector)
        {
            return Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }
        
        public static float LargestComponent(Vector2 vector)
        {
            return Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }
        
        public static float SmallestComponent(Vector3 vector)
        {
            return Mathf.Min(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }
        
        public static float SmallestComponent(Vector2 vector)
        {
            return Mathf.Min(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }
        
        
        public static float ScaleToNormNoClamp(float value, Vector2 range)
        {
            return Mathf.Approximately(range.x, range.y) ? 0 : (value - range.x) / (range.y - range.x);
        }

        public static bool ClampCheck(ref float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
                return true;
            }
            if (value > max)
            {
                value = max;
                return true;
            }
            return false;
        }

        public static bool IsInRange(float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        public static bool IsInRange(float value, Vector2 range)
        {
            return value >= range.x && value <= range.y;
        }

        public static void SortFloats(ref float floatA, ref float floatB)
        {
            if (floatA > floatB)
            {
                (floatB, floatA) = (floatA, floatB);
            }
        }

        public static float FadeInOut(float norm, float inEnd, float outStart)
        {
            norm = Mathf.Clamp01(norm);
            float fade = 1;

            if (inEnd != 0 && norm < inEnd)
                fade = norm / inEnd;

            if (Mathf.Approximately(outStart, 1) && norm > outStart)
                fade = (1 - norm) / (1 - outStart);

            return fade;
        }

        public static float FadeInOut(float normPosition, float inOutPoint)
        {
            return FadeInOut(normPosition, inOutPoint, 1 - inOutPoint);
        }
        
        public static float TangentalSpeedFromQuaternion(Quaternion quaternion)
        {
            quaternion.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
            Vector3 angularDisplacement = rotationAxis * (angleInDegrees * Mathf.Deg2Rad);
            Vector3 angularSpeed = angularDisplacement / Time.deltaTime;
            return angularSpeed.magnitude;
        }
    }
}
