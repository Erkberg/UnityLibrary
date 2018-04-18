using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ErksUnityLibrary
{
    public class TypeWriter : MonoBehaviour
    {
        public Text targetText;

        public TextAudioType textAudioType;
        public enum TextAudioType { None, Speech, Typing }

        public AudioSource audioSource;
        public List<AudioClip> typeClips;

        public TextSkipType textSkipType = TextSkipType.SlowFastSkip;
        public enum TextSkipType { SlowFastSkip, SlowSkip }

        public float waitTime = 0.01f;
        public float waitTimeFast = 0.001f;
        private float currentWaitTime;

        public float specialCharsTimeMuliplier = 5f;
        public List<string> specialChars;

        private string text;

        private bool isTyping = false;

        private void Awake()
        {
            if (!audioSource) audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            CheckTyping();
        }

        private void CheckTyping()
        {
            if (isTyping)
            {
                CheckSpeechPlayback();

                if (Input.GetButtonDown("Fire1"))
                {
                    if (textSkipType == TextSkipType.SlowSkip)
                    {
                        Skip();
                    }
                    else
                    {
                        if (currentWaitTime == waitTime)
                        {
                            currentWaitTime = waitTimeFast;
                        }
                        else
                        {
                            if (currentWaitTime == waitTimeFast)
                            {
                                Skip();
                            }
                        }
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    ClearText();
                    // TODO: callback
                }
            }
        }

        private void CheckSpeechPlayback()
        {
            if (textAudioType == TextAudioType.Speech)
            {
                if (audioSource != null)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayRandomVolumePitch(typeClips.GetRandomItem());
                    }
                }
            }                
        }

        private void Skip()
        {
            Debug.Log("skip");
            StopAllCoroutines();
            targetText.text += text;
            text = "";
            isTyping = false;
            if (audioSource) audioSource.Stop();
        }

        public void SetText(string text)
        {
            currentWaitTime = waitTime;
            this.text = text;
            ClearText();
            isTyping = true;
            StartCoroutine(Type());
        }

        public void ClearText()
        {
            Debug.Log("clear");
            targetText.text = "";
        }

        IEnumerator Type()
        {
            if (text.Length > 0)
            {
                string firstChar = text[0].ToString();
                targetText.text += firstChar;
                CheckTypeSound();

                if (specialChars.Contains(firstChar))
                {
                    yield return new WaitForSeconds(currentWaitTime * specialCharsTimeMuliplier);
                }
                else
                {
                    yield return new WaitForSeconds(currentWaitTime);
                }

                text = text.Substring(1);
                StartCoroutine(Type());
            }
            else
            {
                isTyping = false;
                if (audioSource) audioSource.Stop();
            }
        }

        private void CheckTypeSound()
        {
            if (textAudioType == TextAudioType.Typing)
            {
                if (audioSource)
                {
                    audioSource.PlayOneShotRandomVolumePitch(typeClips.GetRandomItem());
                }
            }
        }
    }
}
