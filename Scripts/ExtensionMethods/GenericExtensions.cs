using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class GenericExtensions
    {
        public static T Next<T>(this T src) where T : Enum
        {
            T[] values = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf(values, src) + 1;
            return (values.Length == j) ? values[0] : values[j];            
        }
        
        public static T Previous<T>(this T src) where T : Enum
        {
            T[] values = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf(values, src) - 1;
            return (j == -1) ? values[values.Length - 1] : values[j];            
        }
		
		public static IEnumerable<T> GetValues<T>() 
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}
    }
}