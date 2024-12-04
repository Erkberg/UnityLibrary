using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class RigidbodyExtensions
    {
        public static void SetVelocityX(this Rigidbody2D rb2D, float value)
        {
            Vector2 velocity = rb2D.GetVelocity();
            velocity.x = value;
            rb2D.SetVelocity(velocity);
        }
        
        public static void SetVelocityY(this Rigidbody2D rb2D, float value)
        {
            Vector2 velocity = rb2D.GetVelocity();
            velocity.y = value;
            rb2D.SetVelocity(velocity);
        }
        
        public static void AddVelocityX(this Rigidbody2D rb2D, float value)
        {
            Vector2 velocity = rb2D.GetVelocity();
            velocity.x += value;
            rb2D.SetVelocity(velocity);
        }
        
        public static void AddVelocityY(this Rigidbody2D rb2D, float value)
        {
            Vector2 velocity = rb2D.GetVelocity();
            velocity.y += value;
            rb2D.SetVelocity(velocity);
        }
        
        public static void SetVelocityX(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.GetVelocity();
            velocity.x = value;
            rb.SetVelocity(velocity);
        }
        
        public static void SetVelocityY(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.GetVelocity();
            velocity.y = value;
            rb.SetVelocity(velocity);
        }
        
        public static void SetVelocityZ(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.GetVelocity();
            velocity.z = value;
            rb.SetVelocity(velocity);
        }
        
        public static void AddVelocityX(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.GetVelocity();
            velocity.x += value;
            rb.SetVelocity(velocity);
        }
        
        public static void AddVelocityY(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.GetVelocity();
            velocity.y += value;
            rb.SetVelocity(velocity);
        }
        
        public static void AddVelocityZ(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.GetVelocity();
            velocity.z += value;
            rb.SetVelocity(velocity);
        }

        public static Vector3 GetVelocity(this Rigidbody rb)
        {
#if UNITY_6000_0_OR_NEWER
            return rb.linearVelocity;
#else
            return rb.velocity;
#endif
        }

        public static void SetVelocity(this Rigidbody rb, Vector3 velo)
        {
#if UNITY_6000_0_OR_NEWER
            rb.linearVelocity = velo;
#else
            rb.velocity = velo;
#endif
        }

        public static Vector3 GetVelocity(this Rigidbody2D rb2D)
        {
#if UNITY_6000_0_OR_NEWER
            return rb2D.linearVelocity;
#else
            return rb2D.velocity;
#endif
        }

        public static void SetVelocity(this Rigidbody2D rb2D, Vector3 velo)
        {
#if UNITY_6000_0_OR_NEWER
            rb2D.linearVelocity = velo;
#else
            rb2D.velocity = velo;
#endif
        }
    }
}

