using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary.HexMap
{
    public static class ListPool<T>
    {
        private static Stack<List<T>> stack = new Stack<List<T>>();

        public static List<T> Get()
        {
            if (stack.Count > 0)
            {
                return stack.Pop();
            }
            return new List<T>();
        }

        public static void Add(List<T> list)
        {
            list.Clear();
            stack.Push(list);
        }
    }
}

