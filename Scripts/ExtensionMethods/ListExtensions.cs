using System;
using System.Collections;
using System.Collections.Generic;

namespace ErksUnityLibrary
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static T GetRandomItem<T>(this List<T> list)
        {
            if (list.Count == 0) return default(T);
            else return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T GetLastItem<T>(this List<T> list)
        {
            if (list.Count == 0) return default(T);
            else return list[list.Count - 1];
        }

        public static void ShuffleList<T>(this List<T> list)
        {
            Random random = new Random();
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}