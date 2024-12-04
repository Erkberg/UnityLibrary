using System.Collections;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class Screenshake : MonoBehaviour
    {
        public ScreenshakeDimension dimension;
        public enum ScreenshakeDimension { TwoD, ThreeD }

        private Vector3 initialShakePosition;
        private float shakeOffset;

        private void Awake()
        {
            initialShakePosition = transform.localPosition;
        }

        public void StartShake(float offset = 0.1f, float duration = 0.1f)
        {
            StopShake();

            initialShakePosition = transform.localPosition;
            shakeOffset = offset;

            StartCoroutine(ShakeSequence(duration));
        }
        
        public void ModifyCurrentShake(float newOffset)
        {
            shakeOffset = newOffset;
        }

        public void AddToCurrentShake(float newOffset)
        {
            shakeOffset += newOffset;
        }

        public void StopShake()
        {
            StopAllCoroutines();
            transform.localPosition = initialShakePosition;
            shakeOffset = 0f;                      
        }

        private IEnumerator ShakeSequence(float duration)
        {
            float durationPassed = 0f;

            while(durationPassed < duration)
            {
                Shake();
                yield return null;
                durationPassed += Time.deltaTime;
            }

            transform.localPosition = initialShakePosition;
        }

        private void Shake()
        {
            if (dimension == ScreenshakeDimension.TwoD)
                transform.localPosition = initialShakePosition + new Vector3(Random.Range(-shakeOffset, shakeOffset), Random.Range(-shakeOffset, shakeOffset), 0f);

            if (dimension == ScreenshakeDimension.ThreeD)
                transform.localPosition = initialShakePosition + new Vector3(Random.Range(-shakeOffset, shakeOffset), Random.Range(-shakeOffset, shakeOffset), Random.Range(-shakeOffset, shakeOffset));
        }
    }
}