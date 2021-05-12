using UnityEngine;

namespace ErksUnityLibrary
{
    public class Screenshake : MonoBehaviour
    {
        public ScreenshakeDimension dimension;
        public enum ScreenshakeDimension { TwoD, ThreeD }

        private Vector3 initialShakePosition;
        private float shakeOffset;

        private void Update()
        {
            if (shakeOffset != 0f) Shake();
        }

        public void StartShake(float offset, float delayTillStop = 0f)
        {
            initialShakePosition = transform.localPosition;
            shakeOffset = offset;

            if (delayTillStop > 0f) Invoke("StopShake", delayTillStop);
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
            shakeOffset = 0f;
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