using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class MaterialExtensions
    {
        public static void SetColorR(this Material mat, float r)
        {
            Color c = mat.color;
            c.r = r;
            mat.color = c;
        }

        public static void SetColorG(this Material mat, float g)
        {
            Color c = mat.color;
            c.g = g;
            mat.color = c;
        }

        public static void SetColorB(this Material mat, float b)
        {
            Color c = mat.color;
            c.b = b;
            mat.color = c;
        }

        public static void SetColorA(this Material mat, float a)
        {
            Color c = mat.color;
            c.a = a;
            mat.color = c;
        }
    }
}