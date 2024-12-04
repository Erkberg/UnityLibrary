using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class PausableRigidbody : MonoBehaviour
    {
        public Rigidbody rb;
        public Rigidbody2D rb2d;

        private Vector3 previousVelocity;
        private Vector2 previousVelocity2d;
        private Vector3 previousAngularVelocity;
        private float previousAngularVelocity2d;

        public void SetPaused(bool paused)
        {
            if (paused)
                Pause();
            else
                Resume();
        }

        public void Pause()
        {
            if(rb)
            {
                previousVelocity = rb.GetVelocity();
                previousAngularVelocity = rb.angularVelocity;
                rb.isKinematic = true;
            }

            if (rb2d)
            {
                previousVelocity2d = rb2d.GetVelocity();
                previousAngularVelocity2d = rb2d.angularVelocity;
                rb2d.isKinematic = true;
            }
        }

        public void Resume()
        {
            if (rb)
            {
                rb.isKinematic = false;
                rb.SetVelocity(previousVelocity);
                rb.angularVelocity = previousAngularVelocity;
                rb.WakeUp();
            }

            if (rb2d)
            {
                rb2d.isKinematic = false;
                rb2d.SetVelocity(previousVelocity2d);
                rb2d.angularVelocity = previousAngularVelocity2d;
                rb2d.WakeUp();
            }
        }

        private void Reset()
        {
            rb = GetComponent<Rigidbody>();
            rb2d = GetComponent<Rigidbody2D>();
        }
    }
}
