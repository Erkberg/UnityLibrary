using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource asMusic;
        public AudioSource asSounds;
        public AudioSource asAmbience;

        public void PlaySound(AudioClip clip)
        {
            asSounds.PlayOneShot(clip);
        }

        public void PlaySoundRandomVolume(AudioClip clip)
        {
            asSounds.PlayOneShotRandomVolume(clip);
        }

        public void PlaySoundRandomPitch(AudioClip clip)
        {
            asSounds.PlayOneShotRandomPitch(clip);
        }

        public void PlaySoundRandomVolumePitch(AudioClip clip)
        {
            asSounds.PlayOneShotRandomVolumePitch(clip);
        }

        public void SetMusicMuted(bool muted)
        {
            asMusic.mute = muted;
        }

        public void SetSoundsMuted(bool muted)
        {
            asSounds.mute = muted;
        }

        public void SetAmbienceMuted(bool muted)
        {
            asAmbience.mute = muted;
        }
    }
}