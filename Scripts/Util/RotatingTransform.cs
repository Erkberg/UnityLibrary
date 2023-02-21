using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class RotatingTransform : MonoBehaviour
    {
        public Vector3 rotationSpeed;
        public bool useUnscaledDeltaTime;

        private void Update()
        {
            transform.Rotate(rotationSpeed * (useUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime));
        }
    }
}