using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class FollowTransform : MonoBehaviour
    {
        public Transform targetTransform;
        public Vector3 offset;
        public UpdateMode updateMode = UpdateMode.LateUpdate;
        public bool followX = true, followY = true, followZ = false;
        public bool followRotation = false;

        public enum UpdateMode
        {
            Update,
            FixedUpdate,
            LateUpdate
        }
        
        private void Update()
        {
            if (updateMode == UpdateMode.Update)
            {
                Follow();
            }
        }

        private void FixedUpdate()
        {
            if (updateMode == UpdateMode.FixedUpdate)
            {
                Follow();
            }
        }

        private void LateUpdate()
        {
            if (updateMode == UpdateMode.LateUpdate)
            {
                Follow();
            }
        }

        private void Follow()
        {
            transform.position = GetPosition();

            if(followRotation)
            {
                transform.rotation = targetTransform.rotation;
            }
        }

        private Vector3 GetPosition()
        {
            Vector3 position = transform.position;

            if (followX)
            {
                position.x = targetTransform.position.x;
                position.x += offset.x;
            }
            
            if (followY)
            {
                position.y = targetTransform.position.y;
                position.y += offset.y;
            }
            
            if (followZ)
            {
                position.z = targetTransform.position.z;
                position.z += offset.z;
            }

            return position;
        }
    }
}

