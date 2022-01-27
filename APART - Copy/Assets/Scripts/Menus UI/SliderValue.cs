using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public TextMeshProUGUI sliderText;

    public float maxValue;

    private void Start()
    {
        GetComponent<Slider>().maxValue = maxValue;
        GetComponent<Slider>().value = maxValue;
    }

    public void UpdateSliderText()
    {
        int sliderVal = (int)GetComponent<Slider>().value;
        sliderText.text = sliderVal.ToString();
    }

    public float GetSlideValue()
    {
        return GetComponent<Slider>().value;
    }
}
