using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextScript : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Lap " + timer.GetLap() + " time: " + timer.GetTime().ToString("0.00");
    }

    private void Update()
    {
        text.text = "Lap " + timer.GetLap() + " time: " + timer.GetTime().ToString("0.00");
    }

}
