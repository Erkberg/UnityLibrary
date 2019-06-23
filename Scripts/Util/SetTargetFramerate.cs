using UnityEngine;

/// <summary>
/// Set the max fps of the game in editor. Useful to simulate slow target devices
/// </summary>
public class SetTargetFramerate : MonoBehaviour
{
    [Range(0, 60)]
    public int framerate = 30;

    private int previousFramerate = 30;

    private void Update()
    {
        if (previousFramerate != framerate)
        {
            previousFramerate = framerate;
            Application.targetFrameRate = framerate;
        }
    }
}