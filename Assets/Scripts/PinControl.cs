using UnityEngine;
using System.Collections;

public class PinControl : MonoBehaviour {

	private const int STATE_PIN_IS_OFF = 0;
	private const int STATE_PIN_IS_ON = 1;
	private const int STATE_PIN_IS_TURNING_ON = 2;
	private const int STATE_PIN_IS_TURNING_OFF = 3;
	private const int STATE_PIN_IS_SHINNING = 4;
	private const int STATE_PIN_IS_SHRINKING = 5;
	private int state = STATE_PIN_IS_OFF;

	private float PIN_ON_MID_INTENSITY = 4f;
	private float PIN_ON_SHRINK_INTENSITY = 2f;
	private float PIN_ON_HIGH_INTENSITY = 8f;

	Fader fader;
	GameObject lightGameObj;
	Light light;
	AudioSource audioSource;
	AudioClip audioClipPinEnter;
	AudioClip audioClipPinSelected;
	// Use this for initialization
	void Start () {
		this.lightGameObj = this.gameObject.transform.Find ("Light").gameObject;
		this.light = this.lightGameObj.GetComponent<Light>();
		this.audioSource = this.lightGameObj.GetComponent<AudioSource>();

		audioClipPinEnter = (AudioClip)Resources.Load("Audio/pin_enter", typeof(AudioClip));

		audioClipPinSelected = (AudioClip)Resources.Load("Audio/pin_select_2", typeof(AudioClip));

	}




	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_PIN_IS_OFF:
			break;
		case STATE_PIN_IS_ON:
			break;
		case STATE_PIN_IS_TURNING_ON:
			if (fader != null) {
				if (fader.update ()) {
					fader = null;
					state = STATE_PIN_IS_ON;
				}
			}
			break;
		case STATE_PIN_IS_TURNING_OFF:
			if (fader != null) {
				if (fader.update ()) {
					fader = null;
					state = STATE_PIN_IS_OFF;
				}
			}
			break;
		case STATE_PIN_IS_SHINNING:
			if (fader != null) {
				if (fader.update ()) {
					fader = null;
				}
			}
			break;
		case STATE_PIN_IS_SHRINKING:
			if (fader != null) {
				if (fader.update ()) {
					fader = null;
					internalMakePinShine ();
					state = STATE_PIN_IS_SHINNING;
				}
			}
			break;
		}


	}



	public void turnOnPinLight() {
		this.audioSource.clip = audioClipPinEnter;
		//this.audioSource.Play ();
		fader = new Fader (light, 0, PIN_ON_MID_INTENSITY, 0.3f);
		state = STATE_PIN_IS_TURNING_ON;
	}

	public void turnOffPinLight() {
		state = STATE_PIN_IS_OFF;
		if (this.light.intensity != 0) {
			fader = new Fader (light, light.intensity, 0, 0.3f);
			state = STATE_PIN_IS_TURNING_OFF;
			this.audioSource.Stop ();
		}
	}

	public void makePinShine() {
		state = STATE_PIN_IS_SHRINKING;

		// shrink pin first
		fader = new Fader (light, PIN_ON_MID_INTENSITY, PIN_ON_SHRINK_INTENSITY, 1f);
	}

	private void internalMakePinShine() {
		this.audioSource.clip = audioClipPinSelected;
		this.audioSource.Play ();
		fader = new Fader (light, PIN_ON_SHRINK_INTENSITY, PIN_ON_HIGH_INTENSITY, 5f);
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

			if (t < fadeTime) {
				t += Time.deltaTime;
				l.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
				return false;
			}

			return true;
		}
	}



}
