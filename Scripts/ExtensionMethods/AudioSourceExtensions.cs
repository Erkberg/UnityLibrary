using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ErksUnityLibrary
{
    public static class AudioSourceExtensions
    {
        public static void PlayOneShotRandomVolume(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f, float baseVolume = 1f)
        {
            audioSource.PlayOneShot(clip, Random.Range(baseVolume - maxDeviation, baseVolume + maxDeviation));
        }

        public static void PlayOneShotRandomPitch(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f, float basePitch = 1f)
        {
            audioSource.pitch = Random.Range(basePitch - maxDeviation, basePitch + maxDeviation);
            audioSource.PlayOneShot(clip);
        }

        public static void PlayOneShotRandomVolumePitch(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f, float basePitch = 1f, float baseVolume = 1f)
        {
            audioSource.pitch = Random.Range(basePitch - maxDeviation, basePitch + maxDeviation);
            audioSource.PlayOneShot(clip, Random.Range(baseVolume - maxDeviation, baseVolume + maxDeviation));
        }

        public static void PlayRandomVolume(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f, float baseVolume = 1f)
        {
            audioSource.volume = Random.Range(baseVolume - maxDeviation, baseVolume + maxDeviation);
            audioSource.clip = clip;
            audioSource.Play();
        }

        public static void PlayRandomPitch(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f, float basePitch = 1f)
        {
            audioSource.pitch = Random.Range(basePitch - maxDeviation, basePitch + maxDeviation);
            audioSource.clip = clip;
            audioSource.Play();
        }

        public static void PlayRandomVolumePitch(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f, float basePitch = 1f, float baseVolume = 1f)
        {
            audioSource.volume = Random.Range(baseVolume - maxDeviation, baseVolume + maxDeviation);
            audioSource.pitch = Random.Range(basePitch - maxDeviation, basePitch + maxDeviation);
            audioSource.clip = clip;
            audioSource.Play();
        }
        
        public static IEnumerator FadeOut(this AudioSource audioSource, float duration, Action onCompleted)
        {
            float initialVolume = audioSource.volume;
            float step = initialVolume / duration;

            while(duration > 0f)
            {
                duration -= Time.deltaTime;
                audioSource.volume -= step * Time.deltaTime;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = initialVolume;
            
            onCompleted?.Invoke();
        }
    }
}