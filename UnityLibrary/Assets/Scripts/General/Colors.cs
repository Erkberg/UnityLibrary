using UnityEngine;

namespace ErksUnityLibrary
{
    public static class Colors
    {
        public static Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }

        #region Concrete colors
        // Taken from https://htmlcolorcodes.com/color-names/
        // Red
        public static readonly Color RedIndianRed = new Color32(205, 92, 92, 255);
        public static readonly Color RedLightCoral = new Color32(240, 128, 128, 255);
        public static readonly Color RedSalmon = new Color32(250, 128, 114, 255);
        public static readonly Color RedDarkSalmon = new Color32(233, 150, 122, 255);
        public static readonly Color RedLightSalmon = new Color32(255, 160, 122, 255);
        public static readonly Color RedCrimson = new Color32(220, 20, 60, 255);
        public static readonly Color RedRed = new Color32(255, 0, 0, 255);
        public static readonly Color RedFirebrick = new Color32(178, 34, 34, 255);
        public static readonly Color RedDarkRed = new Color32(139, 0, 0, 255);

        // Pink
        public static readonly Color PinkPink = new Color32(255, 192, 203, 255);
        public static readonly Color PinkLightPink = new Color32(255, 182, 193, 255);
        public static readonly Color PinkHotPink = new Color32(255, 105, 180, 255);
        public static readonly Color PinkDeepPink = new Color32(255, 20, 147, 255);
        public static readonly Color PinkMediumVioletRed = new Color32(199, 21, 133, 255);
        public static readonly Color PinkPaleVioletRed = new Color32(219, 112, 147, 255);

        // Orange
        public static readonly Color OrangeLightSalmon = new Color32(255, 160, 122, 255);
        public static readonly Color OrangeCoral = new Color32(255, 127, 80, 255);
        public static readonly Color OrangeTomato = new Color32(255, 99, 71, 255);
        public static readonly Color OrangeOrangeRed = new Color32(255, 69, 0, 255);
        public static readonly Color OrangeDarkOrange = new Color32(255, 140, 0, 255);
        public static readonly Color OrangeOrange = new Color32(255, 165, 0, 255);

        // Yellow



        // Purple



        // Green



        // Blue



        // Brown



        // White



        // Gray
        #endregion
    }
}