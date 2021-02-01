using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class MoveTransform : MonoBehaviour
    {
        public Vector3 moveSpeed;

        private void Update()
        {
            transform.position += moveSpeed * Time.deltaTime;
        }
    }
}

