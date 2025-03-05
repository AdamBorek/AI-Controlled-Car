using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSpeedTextScript : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Target speed: " + carController.GetTargetSpeed().ToString("0.00");
    }

    private void Update()
    {
        text.text = "Target speed: " + carController.GetTargetSpeed().ToString("0.00");
    }

}
