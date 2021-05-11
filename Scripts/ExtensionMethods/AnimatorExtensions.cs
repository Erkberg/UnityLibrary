using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public static class AnimatorExtensions
    {
        public static IEnumerator WaitForCurrentAnimationDuration(this Animator animator, int layerIndex = 0)
        {
            while (IsLayerLengthZero(animator, layerIndex))
            {
                yield return null;
            }
            
            AnimationClip clip = animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip;
            yield return new WaitForSeconds(clip.length);
        }
        
        public static IEnumerator WaitForNextAnimationDuration(this Animator animator, int layerIndex = 0)
        {
            while (IsLayerLengthZero(animator, layerIndex))
            {
                yield return null;
            }

            if (animator.GetNextAnimatorClipInfo(layerIndex).Length != 0)
            {
                AnimationClip clip = animator.GetNextAnimatorClipInfo(layerIndex)[0].clip;
                yield return new WaitForSeconds(clip.length);
            }
        }

        public static float GetAnimationClipDuration(this Animator animator, string clipName)
        {
            float duration = 0f;
            RuntimeAnimatorController rac = animator.runtimeAnimatorController;    
            foreach (AnimationClip animClip in rac.animationClips)
            {
                if(animClip.name == clipName)        
                {
                    duration = animClip.length;
                }
            }

            return duration;
        }

        public static IEnumerator WaitUntilAnimationOfNameFinished(this Animator animator, string name, int layerIndex = 0)
        {
            yield return WaitUntilAnimationOfNamePlaying(animator, name, layerIndex);
            // Wait till clip is finished
            while (!IsLayerLengthZero(animator, layerIndex) && animator.IsCurrentClipName(name, layerIndex))
            {
                yield return null;
            }
        }
        
        public static IEnumerator WaitUntilAnimationOfNamePlaying(this Animator animator, string name, int layerIndex = 0)
        {
            // Wait till clip has begun
            while (true)
            {
                if (IsLayerLengthZero(animator, layerIndex))
                {
                    yield return null;
                }
                else if (!animator.IsCurrentClipName(name, layerIndex))
                {
                    yield return null;
                }
                else
                {
                    break;
                }
            }
        }

        public static bool IsCurrentClipName(this Animator animator, string name, int layerIndex = 0)
        {
            return animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.name == name;
        }
        
        public static bool IsCurrentStateName(this Animator animator, string name, int layerIndex = 0)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(name);
        }

        public static bool IsPlaying(this Animator animator, int layerIndex = 0)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 1;
        }
        
        private static bool IsLayerLengthZero(Animator animator, int layerIndex = 0)
        {
            return animator.GetCurrentAnimatorClipInfo(layerIndex).Length == 0;
        }
        
        public static bool ContainsParameterOfName(this Animator animator, string parameterName)
        {
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.name == parameterName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}