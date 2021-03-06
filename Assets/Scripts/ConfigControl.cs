﻿using UnityEngine;
using System.Collections;
using System;

public class ConfigControl : MonoBehaviour {
    public const string KEY_CLIENT = "Client";
    public OVRCameraRig ovrCameraRig;
    public Camera publicCamera;
	public Canvas canvas;
    // Use this for initialization
    void Start () {
        Application.runInBackground = true;
        if (isClient())
        {
            //Screen.SetResolution(1920, 1080, false);

            GameObject gameObject = GameObject.Find("StreetViewSphere");
            gameObject.GetComponent<JourneyClientControl>().enabled = true;
            gameObject.GetComponent<JourneyControl>().enabled = false;
            gameObject.GetComponent<UDPReceive>().enabled = true;

            gameObject = GameObject.Find("WorldGlobe");
            gameObject.GetComponent<GlobeControl>().enabled = false;

            gameObject = GameObject.Find("OVRCameraRigGlobe");
            if (gameObject != null)
            {
                gameObject.SetActive(false);

            }
            publicCamera.gameObject.SetActive(true);

			canvas.gameObject.SetActive (true);


            gameObject = GameObject.Find("RenderSettings");
            gameObject.SetActive(false);
        }
        else
        {
            ovrCameraRig.gameObject.SetActive(true);

            GameObject gameObject = GameObject.Find("StreetViewSphere");
            gameObject.GetComponent<UDPSend>().enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public static bool isClient()
    {
        return Convert.ToBoolean(PreviewLabs.PlayerPrefs.GetString(KEY_CLIENT));
    }
}
