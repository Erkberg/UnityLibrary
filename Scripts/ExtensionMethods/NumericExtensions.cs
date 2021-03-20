using UnityEngine;

namespace ErksUnityLibrary
{
    public static class NumericExtensions
    {
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float Squared(this float f)
        {
            return f * f;
        }

        public static int Squared(this int i)
        {
            return i * i;
        }

        public static int GetDirection(float origin, float target)
        {
            int direction = 0;

            if (target < origin)
            {
                direction = -1;
            }
            if (target > origin)
            {
                direction = 1;
            }

            return direction;
        }

        public static int GetDirectionFromFloat(float value)
        {
            int direction = 0;

            if (value < 0f)
            {
                direction = -1;
            }
            if (value > 0f)
            {
                direction = 1;
            }

            return direction;
        }

        public static bool IsBetweenValues(this float value, float lower, float upper, bool includeBorders)
        {
            if (includeBorders)
            {
                if (value >= lower && value <= upper)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (value > lower && value < upper)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsApproxEqual(this float value, float otherValue)
        {
            return Mathf.Abs(value - otherValue) < 0.001f;
        }

        public static string ToLeadingZeroString(this int value)
        {
            return value < 10 ? $"0{value}" : value.ToString();
        }
    }
}