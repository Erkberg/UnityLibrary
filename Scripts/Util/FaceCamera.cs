using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class FaceCamera : MonoBehaviour
    {
        public Transform cam;

        private void Start()
        {
            if (!cam)
                cam = Camera.main.transform;
        }

        private void Update()
        {
            transform.LookAt(cam);
        }
    }
}