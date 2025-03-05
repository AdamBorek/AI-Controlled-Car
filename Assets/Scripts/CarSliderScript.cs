using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarSliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text sliderText;

    [SerializeField] private CarController carController;

    [SerializeField] private float whichStat;

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

        switch(whichStat)
        {
            case 0:
                slider.onValueChanged.AddListener(delegate { carController.SetMaxSpeed(slider.value); });
                break;

            case 1:
                slider.onValueChanged.AddListener(delegate { carController.SetAcceleration(slider.value); });
                break;

            case 2:
                slider.onValueChanged.AddListener(delegate { carController.SetDeacceleration(slider.value); });
                break;

            case 3:
                slider.onValueChanged.AddListener(delegate { carController.SetMaxSteer(slider.value); });
                break;

            case 4:
                slider.onValueChanged.AddListener(delegate { carController.SetSteerAcceleration(slider.value); });
                break;

            default:
                Debug.LogWarning("CAR SLIDER NOT SET UP CORRECTLY");
                break;
        }

    }

    public void Reset()
    {
        slider.value = startValue;
    }
}
