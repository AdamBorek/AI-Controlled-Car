using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaySliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text sliderText;

    [SerializeField] private FuzzyLogic fuzzy;

    float startValue;

    // Start is called before the first frame update
    void Start()
    {
        startValue = slider.value;

        sliderText.text = slider.value.ToString("0.00");

        slider.onValueChanged.AddListener((value) =>
        {
            sliderText.text = value.ToString("0.00");
        });

        slider.onValueChanged.AddListener(delegate { fuzzy.SetUpData(); });
    }

    public void Reset()
    {
        slider.value = startValue;
    }
}
