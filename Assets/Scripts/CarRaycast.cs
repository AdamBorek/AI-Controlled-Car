using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarRaycast : MonoBehaviour
{
    // data about raycast
    [SerializeField] float angle;
    [SerializeField] Slider nearSlider;
    [SerializeField] Slider farSlider;
    [SerializeField] Vector3 offset;

    // hit data;
    RaycastHit hit;
    [SerializeField] RayCastDataManager dataManager;

    // rendering
    Cast cast;
    LineRenderer lineRend;

    private void Awake()
    {
        // set up renderer
        lineRend = gameObject.GetComponent<LineRenderer>();
        cast = gameObject.AddComponent<Cast>();

        // set up raycast
        cast.SetNear(nearSlider.value);
        cast.SetFar(farSlider.value);

        // update the cast
        FixedUpdate();
    }

    private void RayCastFuzzy(Vector3 position, Vector3 direction, LineRenderer lineRenderer)
    {
        // set up renderer for the raycast
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, position);
        lineRend.startWidth = 0.2f;
        lineRend.endWidth = 0.2f;

        // shoot a raycast in front of the car
        if (Physics.Raycast(position, direction, out hit, float.PositiveInfinity)) // if raycast hits
        {
            // set the end position of the raycast to the hitpoint
            lineRenderer.SetPosition(1, hit.point);

            if (hit.distance > cast.GetFar()) // further than far
            {
                lineRenderer.material.SetColor("_Color", Color.green); // set the colour to green
            }
            else if (hit.distance < cast.GetFar() && hit.distance > cast.GetNear()) // between far and near
            {
                lineRenderer.material.SetColor("_Color", Color.yellow); // set the colour to yellow
            }
            else // below near
            {
                lineRenderer.material.SetColor("_Color", Color.red); // set the colour to red
            }

            // set the angle and distance for the raycast
            cast.SetAngle(angle);
            cast.SetDistance(hit.distance);

            // add raycast to the data manager's list
            dataManager.AddCast(cast);
        }
        else // if raycast doesnt hit
        {
            // set raycast to maximum value
            lineRenderer.SetPosition(1, position + (direction * cast.GetFar()));
            lineRenderer.material.SetColor("_Color", Color.white); // set colour to white
        }

    }

    private void FixedUpdate()
    {
        // set raycast values
        cast.SetNear(nearSlider.value);
        cast.SetFar(farSlider.value);

        // calculate position and forward vector
        Vector3 pos = transform.position + offset;
        Vector3 angleVec = Quaternion.AngleAxis(transform.eulerAngles.y + angle, Vector3.up) * Vector3.forward;

        // render the raycast
        RayCastFuzzy(pos, angleVec, lineRend);
    }

    public Cast GetCast()
    {
        return cast;
    }
}
