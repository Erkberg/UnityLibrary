using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    private int direction = 0;
    private float fadeSpeed = 1f;
    private float threshold = 1f;

    private Image image;

    private bool destroyAfterFade = false;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Fade();
    }

    private void Fade()
    {
        if (direction != 0)
        {
            Color color = image.color;
            color.a += direction * Time.deltaTime * fadeSpeed;

            if (color.a <= 0f)
            {
                color.a = 0f;
                direction = 0;

                if (destroyAfterFade) Destroy(gameObject);
            }

            if (color.a >= threshold)
            {
                color.a = threshold;
                direction = 0;

                if (destroyAfterFade) Destroy(gameObject);
            }

            image.color = color;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inOrOut">"in" or "out"</param>
    /// <param name="destroyAfterFade"></param>
    /// <param name="fadeSpeed"></param>
    /// <param name="threshold"></param>
    public void StartFade(string inOrOut, bool destroyAfterFade, float fadeSpeed, float threshold = 1f)
    {
        this.destroyAfterFade = destroyAfterFade;
        this.fadeSpeed = fadeSpeed;
        this.threshold = threshold;

        if (inOrOut.Equals("in"))
        {
            direction = 1;
        }

        if (inOrOut.Equals("out"))
        {
            direction = -1;
        }
    }
}
