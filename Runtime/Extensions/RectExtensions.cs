using UnityEngine;

namespace MaxVram.Extensions
{
    public static class RectExtensions
    {
        /// <summary>
        /// Returns an array of Rects with equal width spaced evenly across area of the specified Rect.
        /// </summary>
        /// <param name="rect">The Rect to divide.</param>
        /// <param name="divisions">The number of Rects to divide the space into.</param>
        /// <returns></returns>
        public static Rect[] Divide(this Rect rect, int divisions)
        {
            float divWidth = rect.width / divisions;
            var rects = new Rect[divisions];

            for (var i = 0; i < divisions; i++)
                rects[i] = new Rect(rect.x + divWidth * i, rect.y, divWidth - 2, rect.height);

            return rects;
        }

        /// <summary>
        /// Returns a new Rect object with an xMin and xMax defined as the minimum and maximum x values within the Rect array.
        /// </summary>
        /// <param name="rects">The Rect array to combine.</param>
        /// <returns>A single Rect object.</returns>
        public static Rect Combine(this Rect[] rects)
        {
            var rect = new Rect(rects[0]);
            float xMin = rect.xMin;
            float xMax = rect.xMax;

            foreach (Rect r in rects)
            {
                xMin = xMin < r.xMin ? xMin : r.xMin;
                xMax = xMax > r.xMax ? xMax : r.xMax;
            }

            return rect;
        }

        /// <summary>
        /// Returns a smaller Rect with its width and xPos defined as a fraction of the specified divisions.
        /// </summary>
        /// <param name="rect">The rect to divide.</param>
        /// <param name="divisions">The number of divisions to split the rect into.</param>
        /// <param name="startIndex">The index of the division that provides the returned rect's xPos.</param>
        /// <param name="width">The number of divisions to define the returned rect's width.</param>
        /// <returns>A new Rect object.</returns>
        public static Rect GetRect(this Rect rect, int divisions, int startIndex, int width)
        {
            Rect returnRect = rect;
            float divWidth = returnRect.width / divisions;
            returnRect = new Rect(returnRect.x + divWidth * startIndex, returnRect.y, divWidth * width - 2, returnRect.height);
            return returnRect;
        }
    }
}