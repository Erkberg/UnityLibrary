using UnityEngine;

namespace ErksUnityLibrary
{
    public class DestroyAfterSeconds : MonoBehaviour
    {
        public float delay = 5f;

        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}