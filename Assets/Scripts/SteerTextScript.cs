using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteerTextScript : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Steer " + carController.GetTargetSteer().ToString("0.00");
    }

    private void Update()
    {
        text.text = "Steer " + carController.GetTargetSteer().ToString("0.00");
    }

}
