using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class TransformExtensions
    {
        // Position
        public static void SetPositionX(this Transform transform, float x)
        {
            Vector3 position = transform.position;
            position.x = x;
            transform.position = position;
        }

        public static void SetPositionY(this Transform transform, float y)
        {
            Vector3 position = transform.position;
            position.y = y;
            transform.position = position;
        }

        public static void SetPositionZ(this Transform transform, float z)
        {
            Vector3 position = transform.position;
            position.z = z;
            transform.position = position;
        }

        // Local position
        public static void SetLocalPositionX(this Transform transform, float x)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x = x;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionY(this Transform transform, float y)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }

        // Rotation
        public static void SetRotationX(this Transform transform, float x)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.x = x;
            transform.eulerAngles = rotation;
        }

        public static void SetRotationY(this Transform transform, float y)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.y = y;
            transform.eulerAngles = rotation;
        }

        public static void SetRotationZ(this Transform transform, float z)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.z = z;
            transform.eulerAngles = rotation;
        }

        // Local rotation
        public static void SetLocalRotationX(this Transform transform, float x)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.x = x;
            transform.localEulerAngles = localRotation;
        }

        public static void SetLocalRotationY(this Transform transform, float y)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.y = y;
            transform.localEulerAngles = localRotation;
        }

        public static void SetLocalRotationZ(this Transform transform, float z)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.z = z;
            transform.localEulerAngles = localRotation;
        }

        // Local scale
        public static void SetScale(this Transform transform, float scale)
        {
            transform.SetScaleX(scale);
            transform.SetScaleY(scale);
            transform.SetScaleZ(scale);
        }

        public static void SetScaleX(this Transform transform, float x)
        {
            Vector3 scale = transform.localScale;
            scale.x = x;
            transform.localScale = scale;
        }

        public static void SetScaleY(this Transform transform, float y)
        {
            Vector3 scale = transform.localScale;
            scale.y = y;
            transform.localScale = scale;
        }

        public static void SetScaleZ(this Transform transform, float z)
        {
            Vector3 scale = transform.localScale;
            scale.z = z;
            transform.localScale = scale;
        }

        public static List<Transform> GetAllChildren(this Transform transform)
        {
            List<Transform> list = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                list.Add(transform.GetChild(i));
            }
            return list;
        }
    }
}