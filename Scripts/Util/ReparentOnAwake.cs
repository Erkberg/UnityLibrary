using UnityEngine;

namespace ErksUnityLibrary
{
    public class ReparentOnAwake : MonoBehaviour
    {
        public Transform newParent;

        private void Awake()
        {
            transform.parent = newParent;
        }
    }
}
