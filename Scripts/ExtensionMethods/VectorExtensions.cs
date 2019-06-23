using UnityEngine;

namespace ErksUnityLibrary
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3(this Vector2 v2)
        {
            return new Vector3(v2.x, v2.y, 0f);
        }

        public static Vector2 ToVector2(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        public static Vector3 SetX(this Vector3 v3, float x)
        {
            v3.x = x;
            return v3;
        }

        public static Vector3 SetY(this Vector3 v3, float y)
        {
            v3.y = y;
            return v3;
        }

        public static Vector3 SetZ(this Vector3 v3, float z)
        {
            v3.z = z;
            return v3;
        }

        public static Vector2 SetX(this Vector2 v2, float x)
        {
            v2.x = x;
            return v2;
        }

        public static Vector2 SetY(this Vector2 v2, float y)
        {
            v2.y = y;
            return v2;
        }
    }
}