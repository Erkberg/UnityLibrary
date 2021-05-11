using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimatorSpeed : MonoBehaviour
{
    public Animator animator;
    public float minValue = 0.67f;
    public float maxValue = 1.33f;
    
    void Awake()
    {
        if (!animator)
            animator = GetComponent<Animator>();

        if (animator)
            animator.speed = Random.Range(minValue, maxValue);
    }    
}
