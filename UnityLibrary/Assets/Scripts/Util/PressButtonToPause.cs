using UnityEngine;

public class PressButtonToPause : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.P;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
            Debug.Break();
    }
}