using UnityEngine;

public class Animations : MonoBehaviour
{
    public static float GetAnimatorClipLength(Animator animator, string name)
    {
        float time = 0f;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == name)
            {
                time = ac.animationClips[i].length;
            }
        }

        return time;
    }
}
