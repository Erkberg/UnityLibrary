using System;

namespace ErksUnityLibrary
{
    public static class Timing
    {
        public static void AddTimeAndCheckMax(ref float parameter, float maximum, float deltaTime, Action callback)
        {
            parameter += deltaTime;

            if (parameter >= maximum)
            {
                parameter -= maximum;
                callback();
            }
        }
    }
}