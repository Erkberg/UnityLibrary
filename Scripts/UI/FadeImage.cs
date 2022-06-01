using System;
using UnityEngine;
using UnityEngine.UI;

namespace ErksUnityLibrary
{
    public class FadeImage : MonoBehaviour
    {
        public Image image;
        public bool useUnscaledDeltaTime;

        private int direction = 0;
        private float fadeSpeed = 1f;
        private float threshold = 1f;

        private bool destroyAfterFade = false;
        private Action onComplete;

        void Awake()
        {
            if(!image)
            {
                image = GetComponent<Image>();
            }            
        }

        // Update is called once per frame
        void Update()
        {
            if (direction != 0)
            {
                Fade();
            }            
        }

        private void Fade()
        {
            float time = useUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;

            Color color = image.color;
            color.a += direction * time * fadeSpeed;

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

            image.color = color;
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
                SetDirection(1);
            }

            if (inOrOut.Equals("out"))
            {
                SetDirection(-1);
            }
        }

        private void SetDirection(int dir)
        {
            direction = dir;
        }

        public bool IsFading()
        {
            return direction != 0;
        }
        
        public void StopFade()
        {
            SetDirection(0);
            onComplete = null;
        }

        public void SetAlpha(float value)
        {
            Color color = image.color;
            color.a = value;
            image.color = color;
        }
    }
}