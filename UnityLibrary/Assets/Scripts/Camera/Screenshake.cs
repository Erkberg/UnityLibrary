using UnityEngine;

namespace ErksUnityLibrary
{
    public class Screenshake : MonoBehaviour
    {
        private Vector3 originalCameraPosition;

        private float shakeAmp = 0f;

        void Start()
        {
            originalCameraPosition = Camera.main.transform.position;
        }

        public void Shake(float duration, float amp, Vector3 origin, float repeatRate = 0.01f)
        {
            origin.z = transform.position.z;
            originalCameraPosition = origin;
            shakeAmp = amp;
            InvokeRepeating("CameraShake", 0, repeatRate);
            Invoke("StopShaking", duration);
        }

        void CameraShake()
        {
            if (shakeAmp > 0f)
            {
                Vector3 pp = originalCameraPosition;
                float quakeAmp = Random.Range(-shakeAmp, shakeAmp);
                pp.x += quakeAmp;
                quakeAmp = Random.Range(-shakeAmp, shakeAmp);
                pp.y += quakeAmp;
                transform.position = pp;
            }
        }

        void StopShaking()
        {
            CancelInvoke("CameraShake");
            transform.position = originalCameraPosition;
        }
    }
}