using System.Diagnostics;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera downCarCam;
    [SerializeField] private Camera downCam;

    void Start()
    {
        mainCam.gameObject.SetActive(true);
        downCarCam.gameObject.SetActive(false);
        downCam.gameObject.SetActive(false);
    }

    public void SwitchCamera()
    {
        if (mainCam.gameObject.activeSelf)
        {
            mainCam.gameObject.SetActive(false);
            downCarCam.gameObject.SetActive(true);
            return;
        }

        if (downCarCam.gameObject.activeSelf)
        {
            downCarCam.gameObject.SetActive(false);
            downCam.gameObject.SetActive(true);
            return;
        }

        if (downCam.gameObject.activeSelf)
        {
            downCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);
            return;
        }
    }

    private void Update()
    {
    }
}
