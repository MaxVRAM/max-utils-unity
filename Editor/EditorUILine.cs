using UnityEditor;
using UnityEngine;

namespace MaxVram.FancyGUI
{
    public struct MaxGUI
    {
        /// <summary>
        /// Draw a horizontal line in the editor.
        /// </summary>
        /// <param name="color">UnityEngine color to draw the line with.</param>
        /// <param name="thickness">Thickness of the line in pixels.</param>
        /// <param name="padding">Padding from the top and bottom of the line in pixels.</param>
        public static void EditorUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding * 0.5f;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }
    }
}
