using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Debug class to change the timeScale ingame. Please note that this doesn't affect scripts that use realtime.
/// </summary>
public class ChangeTimescale : MonoBehaviour
{
    [Range(0f, 8f)]
    public float timeScale = 1f;

    private float previousScale = 1f;

    private void Start()
    {
        timeScale = Time.timeScale;
        previousScale = timeScale;
    }

    private void Update()
    {
        if (previousScale != timeScale)
        {
            previousScale = timeScale;
            Time.timeScale = timeScale;
        }
    }
}