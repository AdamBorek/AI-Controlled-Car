using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text sliderText;

    // Start is called before the first frame update
    void Start()
    {
        sliderText.text = slider.value.ToString("0.00");

        slider.onValueChanged.AddListener((value) =>
        {
            sliderText.text = value.ToString("0.00");
            Time.timeScale = slider.value;
        });
    }

    public void Reset()
    {
        slider.value = 1f;
    }
}
