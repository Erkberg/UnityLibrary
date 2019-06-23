using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class AudioSourceExtensions
    {
        public static void PlayOneShotRandomVolume(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f)
        {
            audioSource.PlayOneShot(clip, Random.Range(1f - maxDeviation, 1f + maxDeviation));
        }

        public static void PlayOneShotRandomPitch(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f)
        {
            audioSource.pitch = Random.Range(1f - maxDeviation, 1f + maxDeviation);
            audioSource.PlayOneShot(clip);
        }

        public static void PlayOneShotRandomVolumePitch(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f)
        {
            audioSource.pitch = Random.Range(1f - maxDeviation, 1f + maxDeviation);
            audioSource.PlayOneShot(clip, Random.Range(1f - maxDeviation, 1f + maxDeviation));
        }

        public static void PlayRandomVolume(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f)
        {
            audioSource.volume = Random.Range(1f - maxDeviation, 1f + maxDeviation);
            audioSource.clip = clip;
            audioSource.Play();
        }

        public static void PlayRandomPitch(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f)
        {
            audioSource.pitch = Random.Range(1f - maxDeviation, 1f + maxDeviation);
            audioSource.clip = clip;
            audioSource.Play();
        }

        public static void PlayRandomVolumePitch(this AudioSource audioSource, AudioClip clip, float maxDeviation = 0.1f)
        {
            audioSource.volume = Random.Range(1f - maxDeviation, 1f + maxDeviation);
            audioSource.pitch = Random.Range(1f - maxDeviation, 1f + maxDeviation);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}