using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTrack : MonoBehaviour
{
    [SerializeField] CarController controller;

    public void Change()
    {
        if (SceneManager.GetActiveScene().name == "Bahrain")
        {
            SceneManager.LoadScene("Daytona");
        }
        else
        {
            SceneManager.LoadScene("Bahrain");
        }

        Time.timeScale = 1f;
        controller.Reset();

    }
}
