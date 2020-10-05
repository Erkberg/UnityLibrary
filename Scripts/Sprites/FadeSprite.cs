using System;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class FadeSprite : MonoBehaviour
    {
        private int direction = 0;
        private float fadeSpeed = 1f;
        private float threshold = 1f;

        private SpriteRenderer spriteRenderer;

        private bool destroyAfterFade = false;
        private Action onComplete;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            Fade();
        }

        private void Fade()
        {
            if (direction != 0)
            {
                Color color = spriteRenderer.color;
                color.a += direction * Time.deltaTime * fadeSpeed;

                if (direction == -1)
                {
                    if (color.a <= 0f)
                    {
                        color.a = 0f;
                        OnComplete();
                    }
                    else if (color.a <= threshold)
                    {
                        color.a = threshold;
                        OnComplete();
                    }
                }
                else if (direction == 1)
                {
                    if (color.a >= threshold)
                    {
                        color.a = threshold;
                        OnComplete();
                    }
                }

                spriteRenderer.color = color;
            }
        }

        private void OnComplete()
        {
            direction = 0;

            if (onComplete != null)
            {
                onComplete.Invoke();
                onComplete = null;
            }
                    
            if (destroyAfterFade) Destroy(gameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inOrOut">"in" or "out"</param>
        /// <param name="destroyAfterFade"></param>
        /// <param name="fadeSpeed"></param>
        /// <param name="threshold"></param>
        public void StartFade(string inOrOut, bool destroyAfterFade = false, float fadeSpeed = 1f, float threshold = 1f, Action onComplete = null)
        {
            this.destroyAfterFade = destroyAfterFade;
            this.fadeSpeed = fadeSpeed;
            this.threshold = threshold;
            this.onComplete = onComplete;
            
            if (inOrOut.Equals("in"))
            {
                direction = 1;
            }

            if (inOrOut.Equals("out"))
            {
                direction = -1;
            }
        }

        public void StopFade()
        {
            direction = 0;
            onComplete = null;
        }
        
        public void SetAlpha(float value)
        {
            Color color = spriteRenderer.color;
            color.a = value;
            spriteRenderer.color = color;
        }
    }
}