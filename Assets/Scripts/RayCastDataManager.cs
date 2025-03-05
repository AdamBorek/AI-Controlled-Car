using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDataManager : MonoBehaviour
{
    // create list of raycasts
    private readonly List<Cast> castList = new();

    // all raycasts on the car passed in
    [SerializeField] CarRaycast leftCast1;
    [SerializeField] CarRaycast leftCast2;
    [SerializeField] CarRaycast leftCast3;
    [SerializeField] CarRaycast leftForwardCast;
    [SerializeField] CarRaycast forwardCast;
    [SerializeField] CarRaycast rightForwardCast;
    [SerializeField] CarRaycast rightCast3;
    [SerializeField] CarRaycast rightCast2;
    [SerializeField] CarRaycast rightCast1;

    // all raycasts shrinked down to a smaller number of casts
    private Cast left;
    private Cast leftForward;
    private Cast forward;
    private Cast rightForward;
    private Cast right;

    private bool dataReady;

    // calculate average angle on sides
    private float AvgAngle(bool left)
    {
        float sumOfAngles = 0;
        int allCasts = 0;

        for (int i = 0; i < castList.Count; i++)
        {
            if (left == true)
            {
                if (castList[i].GetAngle() < leftForward.GetAngle())
                {
                    sumOfAngles += castList[i].GetAngle();
                    allCasts++;
                }
            }
            else
            {
                if (castList[i].GetAngle() > rightForward.GetAngle())
                {
                    sumOfAngles += castList[i].GetAngle();
                    allCasts++;
                }
            }
        }

        float avg = sumOfAngles / allCasts;
        return avg;
    }

    // calculate average distance on sides
    private float AvgDistance(bool left)
    {
        float sumOfDistance = 0;
        int allCasts = 0;

        for (int i = 0; i < castList.Count; i++)
        {
            if (left == true)
            {
                if (castList[i].GetAngle() < leftForward.GetAngle())
                {
                    sumOfDistance += castList[i].GetDistance();
                    allCasts++;
                }
            }
            else
            {
                if (castList[i].GetAngle() > rightForward.GetAngle())
                {
                    sumOfDistance += castList[i].GetDistance();
                    allCasts++;
                }
            }
        }

        float avg = sumOfDistance / allCasts;
        return avg;
    }

    // calculate average near value on sides
    private float AvgNear(bool left)
    {
        float sumOfnear = 0;
        int allCasts = 0;

        for (int i = 0; i < castList.Count; i++)
        {
            if (left == true)
            {
                if (castList[i].GetAngle() < leftForward.GetAngle())
                {
                    sumOfnear += castList[i].GetNear();
                    allCasts++;
                }
            }
            else
            {
                if (castList[i].GetAngle() > rightForward.GetAngle())
                {
                    sumOfnear += castList[i].GetNear();
                    allCasts++;
                }
            }
        }

        float avg = sumOfnear / allCasts;
        return avg;
    }

    // calculate average far value on sides
    private float AvgFar(bool left)
    {
        float sumOfFar = 0;
        int allCasts = 0;

        for (int i = 0; i < castList.Count; i++)
        {
            if (left == true)
            {
                if (castList[i].GetAngle() < leftForward.GetAngle())
                {
                    sumOfFar += castList[i].GetFar();
                    allCasts++;
                }
            }
            else
            {
                if (castList[i].GetAngle() > rightForward.GetAngle())
                {
                    sumOfFar += castList[i].GetFar();
                    allCasts++;
                }
            }
        }

        float avg = sumOfFar / allCasts;
        return avg;
    }

    private void Awake()
    {
        dataReady = false;
    }

    private void Start()
    {
        // initialize casts
        left = gameObject.AddComponent<Cast>();
        leftForward = gameObject.AddComponent<Cast>();
        forward = gameObject.AddComponent<Cast>();
        rightForward = gameObject.AddComponent<Cast>();
        right = gameObject.AddComponent<Cast>();

        Debug.Log("Castlist count: " + castList.Count);

        // get casts
        leftForward = leftForwardCast.GetCast();
        rightForward = rightForwardCast.GetCast();

        left.SetAngle(AvgAngle(true));
        left.SetDistance(AvgDistance(true));
        left.SetNear(AvgNear(true));
        left.SetFar(AvgFar(true));

        forward = forwardCast.GetCast();

        right.SetAngle(AvgAngle(false));
        right.SetDistance(AvgDistance(false));
        right.SetNear(AvgNear(false));
        right.SetFar(AvgFar(false));

        Debug.Log("Left distance: " + left.GetDistance());
        Debug.Log("LeftForward distance: " + leftForward.GetDistance());
        Debug.Log("Forward distance: " + forward.GetDistance());
        Debug.Log("RightForward distance: " + rightForward.GetDistance());
        Debug.Log("Right distance: " + right.GetDistance());

        // set data to ready
        dataReady = true;
    }

    private void FixedUpdate()
    {
        // get casts
        leftForward = leftForwardCast.GetCast();
        rightForward = rightForwardCast.GetCast();

        forward = forwardCast.GetCast();

        left.SetAngle(AvgAngle(true));
        left.SetDistance(AvgDistance(true));
        left.SetNear(AvgNear(true));
        left.SetFar(AvgFar(true));

        right.SetAngle(AvgAngle(false));
        right.SetDistance(AvgDistance(false));
        right.SetNear(AvgNear(false));
        right.SetFar(AvgFar(false));

        // clear castlist for the next update
        castList.Clear();
    }

    // getters and setters
    public Cast GetLeft()
    {
        return left;
    }

    public Cast GetLeftForward()
    {
        return leftForward;
    }

    public Cast GetForward()
    {
        return forward;
    }

    public Cast GetRightForward()
    {
        return rightForward;
    }

    public Cast GetRight()
    {
        return right;
    }

    public bool IsDataReady()
    {
        return dataReady;
    }

    public void AddCast(Cast newCast)
    {
        castList.Add(newCast);
    }

}
