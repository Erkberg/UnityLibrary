using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class FlickerRenderers : MonoBehaviour
    {
        public List<Renderer> renderers;
        public int flickerFrames = 12;

        public void StartFlicker(float duration)
        {
            StartCoroutine(FlickerSequence(duration));
        }

        private IEnumerator FlickerSequence(float duration)
        {
            float durationPassed = 0f;
            bool currentlyEnabled = true;

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

            SetRenderersEnabled(true);
        }

        public void StopFlicker()
        {
            StopAllCoroutines();
            SetRenderersEnabled(true);
        }

        private void SetRenderersEnabled(bool enabled)
        {
            foreach (Renderer rend in renderers)
            {
                rend.enabled = enabled;
            }
        }
    }
}
