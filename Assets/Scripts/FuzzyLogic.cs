using System.Collections;
using System.Collections.Generic;
using FLS;
using FLS.Rules;
using FLS.MembershipFunctions;
using UnityEngine;

public class FuzzyLogic : MonoBehaviour
{

    [SerializeField] RayCastDataManager rayData;
    [SerializeField] float sideDistanceOverlap;
    [SerializeField] float sideForwardDistanceOverlap;
    [SerializeField] float forwardDistanceOverlap;

    // Fuzzy logic engines
    FLS.IFuzzyEngine steerEngine;
    FLS.IFuzzyEngine leftSteerEngine;
    FLS.IFuzzyEngine rightSteerEngine;
    FLS.IFuzzyEngine speedEngine;
    
    // linguistic variables
    FLS.LinguisticVariable distanceLeft;
    FLS.LinguisticVariable distanceLeftForward;
    FLS.LinguisticVariable distanceForward;
    FLS.LinguisticVariable distanceRightForward;
    FLS.LinguisticVariable distanceRight;

    FLS.LinguisticVariable speed;
    FLS.LinguisticVariable steering;

    // membership functions
    IMembershipFunction leftNear;
    IMembershipFunction leftMiddle;
    IMembershipFunction leftFar;

    IMembershipFunction leftForwardNear;
    IMembershipFunction leftForwardFar;

    IMembershipFunction forwardNear;
    IMembershipFunction forwardMiddle;
    IMembershipFunction forwardFar;

    IMembershipFunction rightForwardNear;
    IMembershipFunction rightForwardFar;

    IMembershipFunction rightNear;
    IMembershipFunction rightMiddle;
    IMembershipFunction rightFar;

    IMembershipFunction speedSlow;
    IMembershipFunction speedMedium;
    IMembershipFunction speedFast;

    IMembershipFunction steerFullLeft;
    IMembershipFunction steerSlightLeft;
    IMembershipFunction steerNone;
    IMembershipFunction steerSlightRight;
    IMembershipFunction steerFullRight;

    // raycasts
    Cast left;
    Cast leftForward;
    Cast forward;
    Cast rightForward;
    Cast right;

    // output values
    float targetSteer;
    float leftSteer;
    float rightSteer;
    float targetSpeed;

    private bool hasDataBeenSetup;

    private void Awake()
    {
        // data has not been set up on awake
        hasDataBeenSetup = false;

        // initialize raycasts
        left = gameObject.AddComponent<Cast>();
        leftForward = gameObject.AddComponent<Cast>();
        forward = gameObject.AddComponent<Cast>();
        rightForward = gameObject.AddComponent<Cast>();
        right = gameObject.AddComponent<Cast>();
    }
    public void SetUpData()
    {
        // set up all the data for fuzzy logic engines
        Debug.LogWarning("Setting up data");

        // update raycasts
        UpdateCasts();

        Debug.LogWarning("initialized left near: " + left.GetNear() + ", left far: " + left.GetFar());
        Debug.LogWarning("initialized left forward near: " + leftForward.GetNear());
        Debug.LogWarning("initialized forward near: " + forward.GetNear() + ", forward far: " + forward.GetFar());
        Debug.LogWarning("initialized right forward near: " + rightForward.GetNear());
        Debug.LogWarning("initialized right near: " + right.GetNear() + ", right far: " + right.GetFar());

        // set up left distance
        distanceLeft = new FLS.LinguisticVariable("distanceLeft");
        leftNear = distanceLeft.MembershipFunctions.AddTrapezoid("Near", 0, 0, left.GetNear() - sideDistanceOverlap, left.GetNear());
        leftMiddle = distanceLeft.MembershipFunctions.AddTrapezoid("Middle", left.GetNear() - sideDistanceOverlap, left.GetNear(), left.GetFar(), left.GetFar() + sideDistanceOverlap);
        leftFar = distanceLeft.MembershipFunctions.AddTrapezoid("Far", left.GetFar(), left.GetFar() + sideDistanceOverlap, Mathf.Infinity, Mathf.Infinity);
        // set up left diagonal distance
        distanceLeftForward = new FLS.LinguisticVariable("distanceLeftForward");
        leftForwardNear = distanceLeftForward.MembershipFunctions.AddTrapezoid("Near", 0, 0, leftForward.GetNear() - sideForwardDistanceOverlap, leftForward.GetNear());
        leftForwardFar = distanceLeftForward.MembershipFunctions.AddTrapezoid("Far", leftForward.GetNear() - sideForwardDistanceOverlap, leftForward.GetNear(), Mathf.Infinity, Mathf.Infinity);
        // set up forward distance
        distanceForward = new FLS.LinguisticVariable("distanceForward");
        forwardNear = distanceForward.MembershipFunctions.AddTrapezoid("Near", 0, 0, forward.GetNear() - forwardDistanceOverlap, forward.GetNear());
        forwardMiddle = distanceForward.MembershipFunctions.AddTrapezoid("Middle", forward.GetNear() - forwardDistanceOverlap, forward.GetNear(), forward.GetFar(), forward.GetFar() + forwardDistanceOverlap);
        forwardFar = distanceForward.MembershipFunctions.AddTrapezoid("Far", forward.GetFar(), forward.GetFar() + forwardDistanceOverlap, Mathf.Infinity, Mathf.Infinity);
        // set up right diagonal distance
        distanceRightForward = new FLS.LinguisticVariable("distanceRightForward");
        rightForwardNear = distanceRightForward.MembershipFunctions.AddTrapezoid("Near", 0, 0, rightForward.GetNear() - sideForwardDistanceOverlap, rightForward.GetNear());
        rightForwardFar = distanceRightForward.MembershipFunctions.AddTrapezoid("Far", rightForward.GetNear() - sideForwardDistanceOverlap, rightForward.GetNear(), Mathf.Infinity, Mathf.Infinity);
        // set up right distance
        distanceRight = new FLS.LinguisticVariable("distanceRight");
        rightNear = distanceRight.MembershipFunctions.AddTrapezoid("Near", 0, 0, right.GetNear() - sideDistanceOverlap, right.GetNear());
        rightMiddle = distanceRight.MembershipFunctions.AddTrapezoid("Middle", right.GetNear() - sideDistanceOverlap, right.GetNear(), right.GetFar(), right.GetFar() + sideDistanceOverlap);
        rightFar = distanceRight.MembershipFunctions.AddTrapezoid("Far", right.GetFar(), right.GetFar() + sideDistanceOverlap, Mathf.Infinity, Mathf.Infinity);
        // set up speed
        speed = new FLS.LinguisticVariable("speed");
        speedSlow = speed.MembershipFunctions.AddTrapezoid("Slow", 0, 0, 0.25, 0.3);
        speedMedium = speed.MembershipFunctions.AddTrapezoid("Medium", 0.25, 0.3, 0.75, 0.8);
        speedFast = speed.MembershipFunctions.AddTrapezoid("Fast", 0.75, 0.8, 1, 1);
        // set up steering
        steering = new FLS.LinguisticVariable("steering");
        steerFullLeft = steering.MembershipFunctions.AddTrapezoid("Full Left", -1, -1, -0.5, -0.3);
        steerSlightLeft = steering.MembershipFunctions.AddTrapezoid("Slight left", -0.5, -0.3, -0.1, -0.05);
        steerNone = steering.MembershipFunctions.AddTrapezoid("None", -0.1, -0.05, 0.05, 0.1);
        steerSlightRight = steering.MembershipFunctions.AddTrapezoid("Slight right", 0.05, 0.1, 0.3, 0.5);
        steerFullRight = steering.MembershipFunctions.AddTrapezoid("Full Right", 0.3, 0.5, 1, 1);

        // set up engines
        leftSteerEngine = new FuzzyEngineFactory().Default();
        rightSteerEngine = new FuzzyEngineFactory().Default();
        speedEngine = new FuzzyEngineFactory().Default();

        // add rules to left steering engine
        List<FuzzyRule> leftRules = new List<FuzzyRule>();

        leftRules.Add(CreateLeftSteerRule(leftNear, leftForwardNear, steerFullRight));
        leftRules.Add(CreateLeftSteerRule(leftNear, leftForwardFar, steerSlightRight));
        leftRules.Add(CreateLeftSteerRule(leftMiddle, leftForwardNear, steerFullRight));
        leftRules.Add(CreateLeftSteerRule(leftMiddle, leftForwardFar, steerNone));
        leftRules.Add(CreateLeftSteerRule(leftFar, leftForwardNear, steerFullLeft));
        leftRules.Add(CreateLeftSteerRule(leftFar, leftForwardFar, steerNone));

        for (int i = 0; i < leftRules.Count; i++)
        {
            leftSteerEngine.Rules.Add(leftRules[i]);
        }

        // add rules to right steering engine
        List<FuzzyRule> rightRules = new List<FuzzyRule>();

        rightRules.Add(CreateRightSteerRule(rightNear, rightForwardNear, steerFullLeft));
        rightRules.Add(CreateRightSteerRule(rightNear, rightForwardFar, steerSlightLeft));
        rightRules.Add(CreateRightSteerRule(rightMiddle, rightForwardNear, steerFullLeft));
        rightRules.Add(CreateRightSteerRule(rightMiddle, rightForwardFar, steerNone));
        rightRules.Add(CreateRightSteerRule(rightFar, rightForwardNear, steerFullRight));
        rightRules.Add(CreateRightSteerRule(rightFar, rightForwardFar, steerNone));

        for (int i = 0; i < rightRules.Count; i++)
        {
            rightSteerEngine.Rules.Add(rightRules[i]);
        }

        // add rules to speed engine
        var speed1 = CreateSpeedRule(forwardFar, speedFast);
        var speed2 = CreateSpeedRule(forwardMiddle, speedMedium);
        var speed3 = CreateSpeedRule(forwardNear, speedSlow);

        speedEngine.Rules.Add(speed1, speed2, speed3);

        // data has been set up
        hasDataBeenSetup = true;
    }

    private void UpdateCasts()
    {
        // update casts
        left = rayData.GetLeft();
        leftForward = rayData.GetLeftForward();
        forward = rayData.GetForward();
        rightForward = rayData.GetRightForward();
        right = rayData.GetRight();
    }

    
    FuzzyRule CreateLeftSteerRule(IMembershipFunction left, IMembershipFunction leftForward, IMembershipFunction steer)
    {
        return Rule.If(distanceLeft.Is(left).And(distanceLeftForward.Is(leftForward))).Then(steering.Is(steer));
    }

    FuzzyRule CreateRightSteerRule(IMembershipFunction right, IMembershipFunction rightForward, IMembershipFunction steer)
    {
        return Rule.If(distanceRight.Is(right).And(distanceRightForward.Is(rightForward))).Then(steering.Is(steer));
    }

    FuzzyRule CreateSpeedRule(IMembershipFunction forward, IMembershipFunction nspeed)
    {
        return Rule.If(distanceForward.Is(forward)).Then(speed.Is(nspeed));
    }

    private void Update()
    {
        // if data hasnt been set up, and it is ready, set it up
        if (!hasDataBeenSetup)
        {
            if (rayData.IsDataReady())
            {
                Debug.LogWarning("Data is ready for setup");
                SetUpData();
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasDataBeenSetup)
        {
            // update casts
            UpdateCasts();

            // defuzzify left steer
            leftSteer = (float)leftSteerEngine.Defuzzify(new
            {
                distanceLeft = (double)left.GetDistance(),
                distanceLeftForward = (double)leftForward.GetDistance()
            });

            // defuzzify right steer
            rightSteer = (float)rightSteerEngine.Defuzzify(new
            {
                distanceRight = (double)right.GetDistance(),
                distanceRightForward = (double)rightForward.GetDistance()
            });

            // create a target steer with left and right steer values
            targetSteer = leftSteer + rightSteer;

            // defuzzify speed
            targetSpeed = (float)speedEngine.Defuzzify(new
            {
                distanceForward = (double)forward.GetDistance()
            });
        }
    }

    // getters
    public float GetTargetSpeed()
    {
        return targetSpeed;
    }

    public float GetTargetSteer()
    {
        return targetSteer;
    }
}
