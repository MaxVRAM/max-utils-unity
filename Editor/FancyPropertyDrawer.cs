using UnityEditor;
using UnityEngine;

namespace MaxVram.FancyGUI
{
    public class FancyPropertyDrawer : PropertyDrawer
    {
        protected const float Margin = 5;
        protected const int ButtonSize = 16;
        protected const int ButtonFontSize = 9;

        protected static readonly GUIStyle ButtonStyle = new(GUI.skin.button) {
            fontSize = ButtonFontSize, padding = new RectOffset(0, 0, 0, 0)
        };

        protected static bool CheckResetButton(bool showButton, Rect position)
        {
            if (!showButton)
                return false;

            var resetButtonRect = new Rect(position) {
                x = position.x - ButtonSize - Margin, width = ButtonSize, y = position.y + (position.height - ButtonSize) / 2,
                height = ButtonSize
            };

            return GUI.Button(resetButtonRect, "R", ButtonStyle);
        }
    }
}
