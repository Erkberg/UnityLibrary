using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class RectTransformExtensions
    {
        private static Vector3[] worldCorners = new Vector3[4];

        public static bool IsOutOfScreen(this RectTransform rectTransform)
        {
            Rect screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
            
            rectTransform.GetWorldCorners(worldCorners);

            for(int i = 0; i < worldCorners.Length; i++)
            {
                Vector3 corner = worldCorners[i];

                if(!screenRect.Contains(corner))
                {
                    return true;
                }
            }

            return false;
        }
    }
}