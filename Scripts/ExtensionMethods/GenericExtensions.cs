using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}