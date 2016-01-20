using UnityEngine;
using System.Collections;

public class LightsControl : MonoBehaviour {
	Color fromColor;
	Color toColor;

	float fromIntensity;
	float toIntensity;

	float fromBounce;
	float toBounce;

	float fromRange;
	float toRange;

	bool isBlue = true;
	float t = 0;
	float fadeTime = JourneySingleton.SCENE_CHANGE_TIME;

	Light currentLight;

	public Color blueColor;
	public Color redColor;

	public float blueIntensity;
	public float redIntensity;

	public float blueBounce;
	public float redBounce;

	public float blueRange;
	public float redRange;

	bool start = false;

	// Use this for initialization
	void Start () {
		currentLight = this.gameObject.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			if (t < fadeTime) {
				t += Time.deltaTime;

				currentLight.color = Color.Lerp (fromColor, toColor, t / fadeTime);
				currentLight.bounceIntensity = Mathf.Lerp (fromBounce, toBounce, t / fadeTime);
				currentLight.intensity = Mathf.Lerp (fromIntensity, toIntensity, t / fadeTime);
				currentLight.range = Mathf.Lerp (fromRange, toRange, t / fadeTime);
			} else {
				if (isBlue) {
					toRed ();
				} else {
					toBlue ();
				}
			}
		}
	}


	public void toBlue() {
		t = 0;

		fromColor = currentLight.color;
		toColor = blueColor;

		fromIntensity = currentLight.intensity;
		toIntensity = blueIntensity;

		fromBounce = currentLight.bounceIntensity;
		toBounce = blueBounce;

		fromRange = currentLight.range;
		toRange = blueRange;

	}

	public void toRed() {
		t = 0;

		fromColor = currentLight.color;
		toColor = redColor;

		fromIntensity = currentLight.intensity;
		toIntensity = redIntensity;

		fromBounce = currentLight.bounceIntensity;
		toBounce = redBounce;

		fromRange = currentLight.range;
		toRange = redRange;
	}

	public void restart() {
		start = true;
		toRed ();
	}
}
