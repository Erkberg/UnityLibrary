using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCATS
{
    public class Fader : MonoBehaviour
    {
        public State currentState;
        public float currentValue;
        public float fadeSpeed = 1f;
        public float minValue, maxValue;
        public Action onFadedIn, onFadedOut, onFadeFinished;
        public Action<float> onValueChanged;

        private void Start()
        {
            onValueChanged?.Invoke(currentValue);
        }

        protected void Update()
        {
            HandleState();
        }

        protected void HandleState()
        {
            switch (currentState)
            {
                case State.None:
                    break;

                case State.In:
                    currentValue += fadeSpeed * Time.deltaTime;
                    if(currentValue >= maxValue)
                    {
                        currentValue = maxValue;
                        SetState(State.None);
                        onFadedIn?.Invoke();
                        onFadeFinished?.Invoke();
                    }
                    onValueChanged?.Invoke(currentValue);
                    break;

                case State.Out:
                    currentValue -= fadeSpeed * Time.deltaTime;
                    if (currentValue <= minValue)
                    {
                        currentValue = minValue;
                        SetState(State.None);
                        onFadedOut?.Invoke();
                        onFadeFinished?.Invoke();
                    }
                    onValueChanged?.Invoke(currentValue);
                    break;
            }
        }

        public void StopFade()
        {
            SetState(State.None);
        }

        public void FadeIn(Action onFinished = null)
        {
            if(onFinished != null)
            {
                onFadedIn = onFinished;
            }
            SetState(State.In);
        }

        public void FadeOut(Action onFinished = null)
        {
            if (onFinished != null)
            {
                onFadedOut = onFinished;
            }
            SetState(State.Out);
        }

        public void SetState(State state)
        {
            currentState = state;
        }

        public void SetValue(float value)
        {
            currentValue = value;
            onValueChanged?.Invoke(currentValue);
        }

        public void SetFadeSpeed(float speed)
        {
            fadeSpeed = speed;
        }

        public void SetMinValue(float value)
        {
            minValue = value;
        }

        public void SetMaxValue(float value)
        {
            maxValue = value;
        }

        public enum State
        {
            None,
            In,
            Out
        }
    }
}
