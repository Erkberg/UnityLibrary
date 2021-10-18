using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class SpriteRendererExtensions
    {
        public static void SetColorR(this SpriteRenderer sr, float r)
        {
            Color c = sr.color;
            c.r = r;
            sr.color = c;
        }

        public static void SetColorG(this SpriteRenderer sr, float g)
        {
            Color c = sr.color;
            c.g = g;
            sr.color = c;
        }

        public static void SetColorB(this SpriteRenderer sr, float b)
        {
            Color c = sr.color;
            c.b = b;
            sr.color = c;
        }

        public static void SetColorA(this SpriteRenderer sr, float a)
        {
            Color c = sr.color;
            c.a = a;
            sr.color = c;
        }
    }
}