using UnityEngine;

namespace MaxVram.Modules
{
    public static class MaxRandom
    {
        public struct Rando
        {
            public static float Range(Vector2 range) { return Random.Range(range.x, range.y); }
            public static int PickOne(int[] selection) { return selection[Random.Range(0, selection.Length)]; }

            public static int[] RandomIntList(int size = 10, int min = 0, int max = 100) 
            { 
                var outputArray = new int[size];
                for (var i = 0; i < size; i++) { outputArray[i] = Random.Range(min, max); }
                return outputArray;
            }
        }
        
    }
}