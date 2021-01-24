using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class StringExtensions
    {
        private const string ParseNotSuccessfulTemplate = "Couldn't parse {0}";
        private const string Dot = ".";
        private const string Comma = ",";

        public static float ToFloat(this string value, bool showWarning = false)
        {
            float result = 0;
            value = value.Replace(Comma, Dot);

            bool parseSuccessful = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out result);

            if (!parseSuccessful && showWarning)
            {
                Debug.LogWarning(string.Format(ParseNotSuccessfulTemplate, value));
            }

            return result;
        }

        public static int ToInt(this string value, bool showWarning = false)
        {
            int result = 0;
            value = value.Replace(Comma, Dot);

            bool parseSuccessful = int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out result);

            if (!parseSuccessful && showWarning)
            {
                Debug.LogWarning(string.Format(ParseNotSuccessfulTemplate, value));
            }

            return result;
        }

        public static bool ToBool(this string value)
        {
            bool result = value == "true";

            return result;
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}