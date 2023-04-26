using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    public void OnSliderChanged(float value)
    {
        AudioListener.volume = value;
        valueText.text = $"{Mathf.RoundToInt(value * 100)} %";
    }
}
