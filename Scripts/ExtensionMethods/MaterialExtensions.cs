using System;
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

        public static IEnumerator FadeOut(this Material material, float duration, Action onCompleted = null)
        {
            float initialAlpha = material.color.a;
            float currentAlpha = initialAlpha;
            float step = initialAlpha / duration;            

            while (duration > 0f)
            {
                duration -= Time.deltaTime;
                currentAlpha -= step * Time.deltaTime;
                material.SetColorA(currentAlpha);
                yield return null;
            }

            material.SetColorA(0f);

            onCompleted?.Invoke();
        }

        public static IEnumerator FadeIn(this Material material, float duration, float targetAlpha = 1f, Action onCompleted = null)
        {
            float initialAlpha = material.color.a;
            float currentAlpha = initialAlpha;
            float step = (targetAlpha - initialAlpha) / duration;

            while (duration > 0f)
            {
                duration -= Time.deltaTime;
                currentAlpha += step * Time.deltaTime;
                material.SetColorA(currentAlpha);
                yield return null;
            }

            material.SetColorA(targetAlpha);

            onCompleted?.Invoke();
        }
    }
}