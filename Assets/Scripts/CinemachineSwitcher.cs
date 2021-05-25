using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour {
    public CinemachineVirtualCamera preVCam, vCam, boatVCam, introVCam;
    GameObject money, tutorial;

    void Start() {
        boatVCam.Priority = 0;
        vCam.Priority = 0;
        preVCam.Priority = 0;
        introVCam.Priority = 1;
        Invoke("IntroToBoatCam", 2f);

        money = GameObject.Find("Score");
        money.SetActive(false);

        tutorial = GameObject.Find("Tutorial");
        tutorial.SetActive(false);
    }

    void IntroToBoatCam() {
        introVCam.Priority = 0;
        boatVCam.Priority = 1;
        Invoke("HideIntro", 1f);
    }

    void HideIntro() {
        money.SetActive(true);
        tutorial.SetActive(true);
        GameObject.Find("Logo").SetActive(false);
    }

    public void OnGoingDown() {
        boatVCam.Priority = 0;
        vCam.Priority = 0;
        preVCam.Priority = 1;
        Invoke("SwitchToCam2", 0.5f);
    }

    public void SwitchToCam2() {
        boatVCam.Priority = 0;
        vCam.Priority = 1;
        preVCam.Priority = 0;
    }

    public void OnBackUp() {
        boatVCam.Priority = 1;
        vCam.Priority = 0;
        preVCam.Priority = 0;
    }
}
