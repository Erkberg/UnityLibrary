using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCATS
{
    public static class BoundsUtil
    {
        public static bool IsWithinBounds(Vector3 position, Vector3 min, Vector3 max)
        {
            Bounds bounds = new Bounds
            {
                min = min,
                max = max
            };
            return bounds.Contains(position);
        }

        public static bool IsOutsideBounds(Vector3 position, Vector3 min, Vector3 max, bool onlyMaxZ = false)
        {
            if(onlyMaxZ)
            {
                min.z = max.z;
            }

            Bounds bounds = new Bounds
            {
                min = min,
                max = max
            };

            bool drawBounds = false;
            if(drawBounds)
            {
                Debug.DrawLine(min, max, Color.green, 1f);
                //Debug.DrawLine(position, min, Color.red, 1f);
                //Debug.DrawLine(position, max, Color.red, 1f);
            }

            return !bounds.Contains(position);
        }
    }
}
