using UnityEngine;

namespace MaxVram.Modules
{
    public static class MaxChance
    {
        public static float Range(Vector2 range)
        {
            return UnityEngine.Random.Range(range.x, range.y);
        }

        public static int PickOne(int[] selection)
        {
            return selection[UnityEngine.Random.Range(0, selection.Length)];
        }

        public static int[] RandomIntList(int size = 10, int min = 0, int max = 100)
        {
            var outputArray = new int[size];

            for (var i = 0; i < size; i++)
            {
                outputArray[i] = UnityEngine.Random.Range(min, max);
            }

            return outputArray;
        }
    }
}