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
        public static readonly Color YellowGold = new Color32(255, 215, 0, 255);
        public static readonly Color YellowYellow = new Color32(255, 255, 0, 255);
        public static readonly Color YellowLightYellow = new Color32(255, 255, 224, 255);
        public static readonly Color YellowLemonChiffon = new Color32(255, 250, 205, 255);
        public static readonly Color YellowLightGoldenRodyYellow = new Color32(250, 250, 210, 255);
        public static readonly Color YellowPapayaWhip = new Color32(255, 239, 213, 255);
        public static readonly Color YellowMoccasin = new Color32(255, 228, 181, 255);
        public static readonly Color YellowPeachPuff = new Color32(255, 218, 185, 255);
        public static readonly Color YellowPaleGoldenRod = new Color32(238, 232, 170, 255);
        public static readonly Color YellowKhaki = new Color32(240, 230, 140, 255);
        public static readonly Color YellowDarkKhaki = new Color32(189, 183, 107, 255);

        // Purple
        public static readonly Color PurpleLavender = new Color32(230, 230, 250, 255);
        public static readonly Color PurpleThistle = new Color32(216, 191, 216, 255);
        public static readonly Color PurplePlum = new Color32(221, 160, 221, 255);
        public static readonly Color PurpleViolet = new Color32(238, 130, 238, 255);
        public static readonly Color PurpleOrchid = new Color32(218, 112, 214, 255);
        public static readonly Color PurpleFuchsia = new Color32(255, 0, 255, 255);
        public static readonly Color PurpleMagenta = new Color32(255, 0, 255, 255);
        public static readonly Color PurpleMediumOrchid = new Color32(186, 85, 211, 255);
        public static readonly Color PurpleMediumPurple = new Color32(147, 112, 219, 255);
        public static readonly Color PurpleRebeccaPurple = new Color32(102, 51, 153, 255);
        public static readonly Color PurpleBlueViolet = new Color32(138, 43, 226, 255);
        public static readonly Color PurpleDarkViolet = new Color32(148, 0, 211, 255);
        public static readonly Color PurpleDarkOrchid = new Color32(153, 50, 204, 255);
        public static readonly Color PurpleDarkMagenta = new Color32(139, 0, 139, 255);
        public static readonly Color PurplePurple = new Color32(128, 0, 128, 255);
        public static readonly Color PurpleIndigo = new Color32(75, 0, 130, 255);
        public static readonly Color PurpleSlateBlue = new Color32(106, 90, 205, 255);
        public static readonly Color PurpleDarkSlateBlue = new Color32(72, 61, 139, 255);
        public static readonly Color PurpleMediumSlateBlue = new Color32(123, 104, 238, 255);

        // Green



        // Blue



        // Brown



        // White



        // Gray
        #endregion
    }
}