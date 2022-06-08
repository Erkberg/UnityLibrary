using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class RotateAroundTransform : MonoBehaviour
    {
        public Transform target;
        public Vector3 rotationAxis;
        public float rotationSpeed = 1f;

        private void Update()
        {
            transform.RotateAround(target.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }

        [ContextMenu("Randomize rotation axis")]
        private void RandomizeRotationAxis()
        {
            rotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        [ContextMenu("Randomize and normalize rotation axis")]
        private void RandomizeNormalizeRotationAxis()
        {
            rotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}
