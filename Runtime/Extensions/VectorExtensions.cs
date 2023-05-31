using UnityEngine;

namespace MaxVram.Extensions
{
    public static class VectorExtensions
    {
        public static bool FloatWithinRange(this Vector2 range, float input)
        {
            return input >= range.x && input <= range.y;
        }
    }
}
