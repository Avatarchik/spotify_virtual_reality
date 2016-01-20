using UnityEngine;
using System.Collections;

public class RenderControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
    static Color fromColor;
    static Color toColor;
    static float t = 10;
    static float fadeTime = 1;

    static float fromDensity;
    static float toDensity;
    // Update is called once per frame
    void Update () {
		if (t < fadeTime) {
			t += Time.deltaTime;
			RenderSettings.fogColor = Color.Lerp (fromColor, toColor, t / fadeTime);
			Camera.main.backgroundColor = Color.Lerp (fromColor, toColor, t / fadeTime);
			RenderSettings.fogDensity = Mathf.Lerp (fromDensity, toDensity, t / fadeTime);
		}
    }

    public static Color hexToColor(string hex)
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

    public static void setFogToNight()
    {
        t = 0;
        fromColor = RenderSettings.fogColor;
        toColor = hexToColor("131638FF");
        fromDensity = RenderSettings.fogDensity;
        toDensity = 0.035f;

		fadeTime = 15;
    }

    public static void setFogToDay()
    {
        t = 0;
        fromColor = RenderSettings.fogColor;
        toColor = hexToColor("5A63C3FF");
        fromDensity = RenderSettings.fogDensity;
        toDensity = 0.0009f;

		fadeTime = 1;
    }
}
