using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class StringExtensions
    {
        private const string ParseNotSuccessfulTemplate = "Couldn't parse {0}";

        public static float ToFloat(this string value, bool showWarning = false)
        {
            float result = 0;

            bool parseSuccessful = float.TryParse(value, NumberStyles.Float, new CultureInfo("en-US").NumberFormat, out result);

            if (!parseSuccessful && showWarning)
            {
                Debug.LogWarning(string.Format(ParseNotSuccessfulTemplate, value));
            }

            return result;
        }

        public static int ToInt(this string value, bool showWarning = false)
        {
            int result = 0;

            bool parseSuccessful = int.TryParse(value, NumberStyles.Integer, new CultureInfo("en-US").NumberFormat, out result);

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
    }
}