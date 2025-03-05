using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOOB : MonoBehaviour
{
    CarController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -10)
        {
            controller.Reset();
        }
    }
}
