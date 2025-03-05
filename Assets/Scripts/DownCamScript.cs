using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCamScript : MonoBehaviour
{
    Quaternion rotation;

    private void Awake()
    {

        rotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = rotation;
    }
}
