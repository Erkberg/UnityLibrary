using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class FlickerRenderers : MonoBehaviour
    {
        public List<Renderer> renderers;
        public int flickerFrames = 12;

        private bool isFlickering;

        public void StartFlicker(float duration)
        {
            StartCoroutine(FlickerSequence(duration));
        }

        public bool IsFlickering()
        {
            return isFlickering;
        }

        private IEnumerator FlickerSequence(float duration)
        {
            float durationPassed = 0f;
            bool currentlyEnabled = true;
            isFlickering = true;

            while(durationPassed < duration)
            {
                currentlyEnabled = !currentlyEnabled;
                SetRenderersEnabled(currentlyEnabled);

                for (int i = 0; i < flickerFrames; i++)
                {
                    yield return null;
                    durationPassed += Time.deltaTime;
                }                
            }

            isFlickering = false;
            SetRenderersEnabled(true);
        }

        public void StopFlicker()
        {
            StopAllCoroutines();
            isFlickering = false;
            SetRenderersEnabled(true);
        }

        private void SetRenderersEnabled(bool enabled)
        {
            foreach (Renderer rend in renderers)
            {
                rend.enabled = enabled;
            }
        }

        private void Reset()
        {
            renderers = GetComponentsInChildren<Renderer>().ToList();
        }
    }
}
