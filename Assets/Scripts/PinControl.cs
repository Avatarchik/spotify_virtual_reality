using UnityEngine;
using System.Collections;

public class PinControl : MonoBehaviour {

	private string pinName = "Pin_1_Light";

	Fader fader;
	GameObject lightGameObj;
	Light light;

	// Use this for initialization
	void Start () {
		this.lightGameObj = this.gameObject.transform.Find ("Pin_1_Light").gameObject;
		Light[] lights = this.lightGameObj.GetComponents<Light>();
		this.light = lights [0];
	}

	// Update is called once per frame
	void Update () {
		if (fader != null) {
			if (fader.update ()) {
				fader = null;
			}
		}
	}

	public void turnOnPinLight() {
		fader = new Fader (light, 0, 0.5f, 0.3f);
	}

	public void turnOffPinLight() {
		if (this.light.intensity != 0) {
			fader = new Fader (light, 0.5f, 0, 0.3f);
		}
	}

	public void makePinShine() {
		fader = new Fader (light, 0.5f, 2.5f, 2f);
	}

	private class Fader {
		float t = 0.0f;
		Light l;
		float fadeStart;
		float fadeEnd; 
		float fadeTime;
		public Fader(Light l, float fadeStart, float fadeEnd, float fadeTime) {
			this.l = l;
			this.fadeEnd = fadeEnd;
			this.fadeStart = fadeStart;
			this.fadeTime = fadeTime;
		}

		public bool update() {

			while (t < fadeTime) {
				t += Time.deltaTime;
				l.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
				return false;
			}

			return true;
		}
	}



}
