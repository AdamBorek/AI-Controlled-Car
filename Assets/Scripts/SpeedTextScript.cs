using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTextScript : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Speed " + carController.GetCurrentSpeed().ToString("0.00");
    }

    private void Update()
    {
        text.text = "Speed " + carController.GetCurrentSpeed().ToString("0.00");
    }

}
