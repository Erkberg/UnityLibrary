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

        public static Vector2 XY(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }
        public static Vector2 XZ(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }
        public static Vector2 YX(this Vector3 v3)
        {
            return new Vector2(v3.y, v3.x);
        }
        public static Vector2 YZ(this Vector3 v3)
        {
            return new Vector2(v3.y, v3.z);
        }
        public static Vector2 ZX(this Vector3 v3)
        {
            return new Vector2(v3.z, v3.x);
        }
        public static Vector2 ZY(this Vector3 v3)
        {
            return new Vector2(v3.z, v3.y);
        }

        public static Vector3 XY_(this Vector2 v2, float a = 0)
        {
            return new Vector3(v2.x, v2.y, a);
        }
        public static Vector3 X_Y(this Vector2 v2, float a = 0)
        {
            return new Vector3(v2.x, a, v2.y);
        }
        public static Vector3 _XY(this Vector2 v2, float a = 0)
        {
            return new Vector3(a, v2.x, v2.y);
        }
        public static Vector3 YX_(this Vector2 v2, float a = 0)
        {
            return new Vector3(v2.y, v2.x, a);
        }
        public static Vector3 Y_X(this Vector2 v2, float a = 0)
        {
            return new Vector3(v2.y, a, v2.x);
        }
        public static Vector3 _YX(this Vector2 v2, float a = 0)
        {
            return new Vector3(a, v2.y, v2.x);
        }


        public static bool IsApproxEqual(this Vector3 vec, Vector3 otherVec, float precision = Vector3.kEpsilon)
        {
            return ((otherVec - vec).sqrMagnitude < (precision * precision));
        }

        public static bool IsApproxEqual(this Vector2 vec, Vector2 otherVec, float precision = Vector2.kEpsilon)
        {
            return ((otherVec - vec).sqrMagnitude < (precision * precision));
        }

        public static Vector2 RandomVector2()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        public static Vector3 RandomVector3()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}