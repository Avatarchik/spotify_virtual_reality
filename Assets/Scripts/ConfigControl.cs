using UnityEngine;
using System.Collections;
using System;

public class ConfigControl : MonoBehaviour {
    public const string KEY_CLIENT = "Client";

	// Use this for initialization
	void Start () {
        
        if(isClient())
        {
            GameObject gameObject = GameObject.Find("StreetViewSphere");
            gameObject.GetComponent<JourneyClientControl>().enabled = true;
            gameObject.GetComponent<JourneyControl>().enabled = false;

            gameObject = GameObject.Find("WorldGlobe");
            gameObject.GetComponent<GlobeControl>().enabled = false;

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
