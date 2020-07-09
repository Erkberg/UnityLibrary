using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class RigidbodyExtensions
    {
        public static void SetVelocityX(this Rigidbody2D rb2D, float value)
        {
            Vector2 velocity = rb2D.velocity;
            velocity.x = value;
            rb2D.velocity = velocity;
        }
        
        public static void SetVelocityY(this Rigidbody2D rb2D, float value)
        {
            Vector2 velocity = rb2D.velocity;
            velocity.y = value;
            rb2D.velocity = velocity;
        }
        
        public static void SetVelocityX(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.velocity;
            velocity.x = value;
            rb.velocity = velocity;
        }
        
        public static void SetVelocityY(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.velocity;
            velocity.y = value;
            rb.velocity = velocity;
        }
        
        public static void SetVelocityZ(this Rigidbody rb, float value)
        {
            Vector3 velocity = rb.velocity;
            velocity.z = value;
            rb.velocity = velocity;
        }
    }
}

