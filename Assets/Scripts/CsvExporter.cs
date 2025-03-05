using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsvExporter : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] RayCastDataManager raycastDataManager;

    string filename = "";

    TextWriter tw;

    // Start is called before the first frame update
    void Awake()
    {
        string topLine = ("Lap,Halfway time,Time,Max speed,Acceleration,Deacceleration,Max steer,Steer acceleration,Forward far,Forward near,Diagonal near,Side far,Side near");

        filename = Application.dataPath + "/" + SceneManager.GetActiveScene().name + ".csv";
        tw = new StreamWriter(filename, false);
        tw.WriteLine(topLine);
        tw.Close();
    }

    public void AddNewLap(int lap, float halftime, float time)
    {

        tw = new StreamWriter(filename, true);

        string nData = (lap.ToString() + "," + halftime.ToString("0.0") + "," + time.ToString("0.0") + "," + carController.GetMaxSpeed().ToString("0.0") + "," + carController.GetAcceleration().ToString("0.0") + "," + 
                        carController.GetDeacceleration().ToString("0.0") + "," + carController.GetMaxSteer().ToString("0.0") + "," + carController.GetSteerAcceleration().ToString("0.0") + "," + 
                        raycastDataManager.GetForward().GetFar().ToString("0.0") + "," + raycastDataManager.GetForward().GetNear().ToString("0.0") + "," + 
                        raycastDataManager.GetRightForward().GetNear().ToString("0.0") + "," + raycastDataManager.GetRight().GetFar().ToString("0.0") + "," + 
                        raycastDataManager.GetRight().GetNear().ToString("0.0"));

        tw.WriteLine(nData);

        tw.Close();

        Debug.LogWarning("Lap " + lap + " added to the dataset");
    }
}
