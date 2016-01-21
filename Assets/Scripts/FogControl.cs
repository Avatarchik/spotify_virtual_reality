using UnityEngine;
using System.Collections;

public class FogControl : BaseMachine {
	protected const string STATE_NORMAL = "STATE_NORMAL";
	protected const string STATE_FOG_NIGHT = "STATE_FOG_NIGHT";
	protected const string STATE_FOG_DAY = "STATE_FOG_DAY";

	// Use this for initialization
	void Start () {
		this.state = STATE_NORMAL;
	}
    Color fromColor;
    Color toColor;
    float t = 10;
    float fadeTime = 1;

    float fromDensity;
    float toDensity;

	Color ambientLightFromColor;
	Color ambientLightToColor;

	float ambientFromDensity;
	float ambientToDensity;

    // Update is called once per frame
    void Update () {
		switch (this.state) {
		case STATE_FOG_NIGHT:
			if (t < fadeTime) {
				t += Time.deltaTime;
				RenderSettings.fogColor = Color.Lerp (fromColor, toColor, t / fadeTime);
				Camera.main.backgroundColor = Color.Lerp (fromColor, toColor, t / fadeTime);
				RenderSettings.fogDensity = Mathf.Lerp (fromDensity, toDensity, t / fadeTime);
			}
			break;
		case STATE_FOG_DAY:
			if (t < fadeTime) {
				t += Time.deltaTime;
				RenderSettings.fogColor = Color.Lerp (fromColor, toColor, t / fadeTime);
				Camera.main.backgroundColor = Color.Lerp (fromColor, toColor, t / fadeTime);
				RenderSettings.fogDensity = Mathf.Lerp (fromDensity, toDensity, t / fadeTime);
			} else {
				this.state = STATE_NORMAL;
			}
			break;
		case STATE_NORMAL:
			if (t < fadeTime) {
				t += Time.deltaTime;
				RenderSettings.fogColor = Color.Lerp (fromColor, toColor, t / fadeTime);
				Camera.main.backgroundColor = Color.Lerp (fromColor, toColor, t / fadeTime);
				RenderSettings.fogDensity = Mathf.Lerp (fromDensity, toDensity, t / fadeTime);

				RenderSettings.ambientLight = Color.Lerp (ambientLightFromColor, ambientLightToColor, t / fadeTime);
				RenderSettings.ambientIntensity = Mathf.Lerp (ambientFromDensity, ambientToDensity, t / fadeTime);

			}
			break;
		}

	
    }

    public Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

    public void setFogToNight()
    {
        t = 0;
        fromColor = RenderSettings.fogColor;
        toColor = hexToColor("131638FF");
        fromDensity = RenderSettings.fogDensity;
        toDensity = 0.035f;

		fadeTime = 15;

		this.state = STATE_FOG_NIGHT;
    }

    public void setFogToDay()
    {
        t = 0;
        fromColor = RenderSettings.fogColor;
        toColor = hexToColor("5A63C3FF");
        fromDensity = RenderSettings.fogDensity;
        toDensity = 0.0009f;

		fadeTime = 1;

		this.state = STATE_FOG_DAY;
    }

	private void toBlue() {
		/*Fog
		Color: 5A63C3FF
		Density: 0.0009*/
		/*Ambient Light
		Color: 9FFFEBFF
		Density: 0.55*/

		t = 0;
		fromColor = RenderSettings.fogColor;
		toColor = hexToColor("5A63C3FF");
		fromDensity = RenderSettings.fogDensity;
		toDensity = 0.0009f;
		fadeTime = JourneySingleton.SCENE_CHANGE_TIME;

		ambientFromDensity = RenderSettings.ambientIntensity;
		ambientLightFromColor = RenderSettings.ambientLight;

		ambientToDensity = 0.55f;
		ambientLightToColor = hexToColor("9FFFEBFF");
	}

	private void toRed() {
		/*Fog
		Color: C43269FF
		Density: 0.0009*/
		/*
		Ambient Light
		Color: FFF6B2FF
		Density: 0.55*/

		t = 0;
		fadeTime = JourneySingleton.SCENE_CHANGE_TIME;
		fromColor = RenderSettings.fogColor;
		toColor = hexToColor("C43269FF");
		fromDensity = RenderSettings.fogDensity;
		toDensity = 0.0009f;

		ambientFromDensity = RenderSettings.ambientIntensity;
		ambientLightFromColor = RenderSettings.ambientLight;

		ambientToDensity = 0.55f;
		ambientLightToColor = hexToColor("FFF6B2FF");
	}
		
	public void changeFog(bool isRed) {
        if (this.state != STATE_NORMAL)
        {
            return;
        }
		if (isRed) {
			toRed ();
		} else {
			toBlue ();
		}
	}
}
