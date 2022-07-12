using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RCATS
{
    public static class ImageExtensions
    {
        public static void SetColorR(this Image img, float r)
        {
            Color c = img.color;
            c.r = r;
            img.color = c;
        }

        public static void SetColorG(this Image img, float g)
        {
            Color c = img.color;
            c.g = g;
            img.color = c;
        }

        public static void SetColorB(this Image img, float b)
        {
            Color c = img.color;
            c.b = b;
            img.color = c;
        }

        public static void SetColorA(this Image img, float a)
        {
            Color c = img.color;
            c.a = a;
            img.color = c;
        }
    }
}
