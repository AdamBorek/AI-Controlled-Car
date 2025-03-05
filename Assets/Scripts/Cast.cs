using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
    // data of the cast
    private float angle;
    private float distance;
    private float far;
    private float near;

    // getters and setters
    public void SetFar(float newFar)
    {
        far = newFar;
    }

    public void SetNear(float newNear)
    {
        near = newNear;
    }

    public float GetFar()
    {
        return far;
    }

    public float GetNear()
    {
        return near;
    }

    public void SetAngle(float newAngle)
    {
        angle = newAngle;
    }

    public float GetAngle()
    {
        return angle;
    }

    public void SetDistance(float newDistance)
    {
        distance = newDistance;
    }

    public float GetDistance()
    {
        return distance;
    }

}
