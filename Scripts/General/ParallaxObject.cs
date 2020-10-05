using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ErksUnityLibrary
{
    public class ParallaxObject : MonoBehaviour
    {
        public Transform reference;
        public Vector3 speed;
        [Space] 
        public bool looping = false;
        public Vector3 loopOffset;
        public UnityEvent onLoop;

        private Vector3 previousReferencePosition;
        private bool firstFrame = true;

        private void Start()
        {
            if (reference == null)
            {
                reference = Camera.main.transform;
            }
        }

        private void LateUpdate()
        {
            if (firstFrame)
            {
                firstFrame = false;
                return;
            }
            
            transform.position -= Vector3.Scale((reference.position - previousReferencePosition), speed);
            previousReferencePosition = reference.position;

            if (looping)
            {
                CheckLooping();
            }
        }

        private void CheckLooping()
        {
            Vector3 offsetToApply = Vector3.zero;;
            
            // X
            if (!loopOffset.x.IsApproxEqual(0f))
            {
                float thisX = transform.position.x;
                float refX = reference.position.x;

                if (thisX - refX > loopOffset.x)
                {
                    offsetToApply.x = -loopOffset.x * 2;
                }
                else if (refX - thisX > loopOffset.x)
                {
                    offsetToApply.x = loopOffset.x * 2;
                }
            }
            
            // Y
            if (!loopOffset.y.IsApproxEqual(0f))
            {
                float thisY = transform.position.y;
                float refY = reference.position.y;

                if (thisY - refY > loopOffset.y)
                {
                    offsetToApply.y = -loopOffset.y * 2;
                }
                else if (refY - thisY > loopOffset.y)
                {
                    offsetToApply.y = loopOffset.y * 2;
                }
            }
            
            // Z
            if (!loopOffset.z.IsApproxEqual(0f))
            {
                float thisZ = transform.position.z;
                float refZ = reference.position.z;

                if (thisZ - refZ > loopOffset.z)
                {
                    offsetToApply.z = -loopOffset.z * 2;
                }
                else if (refZ - thisZ > loopOffset.z)
                {
                    offsetToApply.z = loopOffset.z * 2;
                }
            }

            if (offsetToApply != Vector3.zero)
            {
                onLoop?.Invoke();
                transform.position += offsetToApply;
            }
        }
    }
}