using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] BoxCollider FinishLine;
    [SerializeField] BoxCollider Halfway;

    [SerializeField] CsvExporter csvExporter;

    float time;
    float halfwayTime;
    int lap;

    bool hitStart;
    bool hitHalfway;
    

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        halfwayTime = 0;
        hitStart = false;
        hitHalfway = false;
        lap = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit this: " + other.gameObject.name);

        if ((other == FinishLine) && !hitStart)
        {
            hitStart = true;
            Debug.LogWarning("Starting timer!");
        }

        if ((other == Halfway) && !hitHalfway)
        {
            hitHalfway = true;
            halfwayTime = time;
            Debug.LogWarning("Hit halfway! time: " + time);
        }

        if ((other == FinishLine) && hitHalfway)
        {
            //updateTime = false;
            Debug.LogWarning("Run finished! lap" + lap + " time: " + time);
            Debug.LogWarning("Resetting time");

            csvExporter.AddNewLap(lap, halfwayTime, time);
            Debug.LogWarning("Lap added to data");

            hitHalfway = false;
            halfwayTime = 0f;
            time = 0f;

            // record data

            lap++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hitStart)
        {
            time += Time.deltaTime;
            //Debug.Log("Time: " + time);
        }
    }

    public float GetTime()
    {
        return time;
    }

    public float GetHalfwayTime()
    {
        return halfwayTime;
    }

    public int GetLap()
    {
        return lap;
    }

    public void Reset()
    {
        time = 0;
        hitStart = false;
        hitHalfway = false;
        Debug.Log("Timer has been reset");
    }
}
