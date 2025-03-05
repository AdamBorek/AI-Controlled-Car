using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [SerializeField] FuzzyLogic fuzzyLogic;

    //speed
    [Header("Speed")]
    [SerializeField] float maxSpeed = 10f;
    private float currentSpeed;

    [SerializeField] float acceleration = 24f;
    [SerializeField] float deacceleration = 36f;

    // steering
    [Header("Steering")]
    [SerializeField] float maxSteer = 120f;
    [SerializeField] float steerAcceleration = 300f;
    private float currentMaxSteer;
    private float currentSteer;

    // targets
    private float targetSpeed;
    private float targetSteer;

    // reset
    private Vector3 startPos;
    private Vector3 startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        startRotation = gameObject.transform.eulerAngles;
        currentSpeed = 0;
        currentSteer = 0;
    }

    public void Reset()
    {
        gameObject.transform.position = startPos;
        gameObject.transform.eulerAngles = startRotation;
        currentSpeed = 0;
        currentSteer = 0;
        gameObject.GetComponent<Timer>().Reset();
    }

    private void FixedUpdate()
    {
        UpdateInput();
        HandleMovement();
        HandleTurning();

        //Debug.Log("Current speed: " + currentSpeed);
        //Debug.Log("Current steering: " + currentSteer);
    }

    private void UpdateInput()
    {

        targetSpeed = maxSpeed * fuzzyLogic.GetTargetSpeed();

        currentMaxSteer = (1 - (currentSpeed / maxSpeed)) * maxSteer;
        targetSteer = currentMaxSteer * fuzzyLogic.GetTargetSteer();

    }
    private void HandleTurning()
    {

        if (currentSteer < targetSteer)
        {
            currentSteer += steerAcceleration * Time.deltaTime;
            if (currentSteer > targetSteer)
            {
                currentSteer = targetSteer;
            }
        }

        if (currentSteer > targetSteer)
        {
            currentSteer -= steerAcceleration * Time.deltaTime;
            if (currentSteer < targetSteer)
            {
                currentSteer = targetSteer;
            }
        }

        // turn the chair
        Vector3 Rotation = gameObject.transform.eulerAngles;
        Rotation.y += currentSteer * Time.deltaTime;
        gameObject.transform.eulerAngles = Rotation;

    }

    private void HandleMovement()
    {
        // add acceleration to the speed;
        if (currentSpeed < targetSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > targetSpeed)
            {
                currentSpeed = targetSpeed;
            }
        }
        
        if (currentSpeed > targetSpeed)
        {
            currentSpeed -= deacceleration * Time.deltaTime;
            if (currentSpeed < targetSpeed)
            {
                currentSpeed = targetSpeed;
            }
        }

        // set the new position;
        gameObject.transform.position += gameObject.transform.forward * currentSpeed * Time.deltaTime;

    }


    // Getters and setters
    public float GetCurrentSpeed()
    { return currentSpeed; }

    public float GetCurrentSteer()
    { return currentSteer; }

    public float GetTargetSpeed()
    { return targetSpeed; }

    public float GetTargetSteer()
    { return targetSteer; }

    public void SetMaxSpeed(float newMaxSpeed)
    {
        maxSpeed = newMaxSpeed;
        Debug.LogWarning("New max speed: " + maxSpeed);
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public void SetAcceleration(float newAcc)
    {
        acceleration = newAcc;
        Debug.LogWarning("New acceleration: " + acceleration);
    }

    public float GetAcceleration()
    {
        return acceleration;
    }

    public void SetDeacceleration(float newdeacc)
    {
        deacceleration = newdeacc;
        Debug.LogWarning("New deaccleration: " + deacceleration);
    }

    public float GetDeacceleration()
    {
        return deacceleration;
    }

    public void SetMaxSteer(float newMaxSteer)
    {
        maxSteer = newMaxSteer;
        Debug.LogWarning("New max steer: " + maxSteer);
    }

    public float GetMaxSteer()
    {
        return maxSteer;
    }

    public void SetSteerAcceleration(float newSteerAcc)
    {
        steerAcceleration = newSteerAcc;
        Debug.LogWarning("New steer acceleration: " + steerAcceleration);
    }

    public float GetSteerAcceleration()
    {
        return steerAcceleration;
    }
}
