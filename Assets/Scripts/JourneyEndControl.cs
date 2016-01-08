using UnityEngine;
using System.Collections;

public class JourneyEndControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	float fadeTime = 0;
	float t = 0;


	public const int STATE_NEUTRAL = 0;
	public const int STATE_FADING_IN = 1;
	public const int STATE_FADING_OUT = 2;
	public const int STATE_FADED = 3;
	private int state = STATE_NEUTRAL;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.X)) {
			end ();
		}

		switch (state) {
		case STATE_FADING_IN:
			if (t < fadeTime) {
				t += Time.deltaTime;
				float alpha = Mathf.Lerp (0, 1, t / fadeTime);
				setAlpha (alpha);
			} else {
				state = STATE_FADING_OUT;
				t = 0;
			}
			break;
		case STATE_FADING_OUT:
			if (t < fadeTime) {
				t += Time.deltaTime;
				float alpha = Mathf.Lerp (1, 0, t / fadeTime);
				setAlpha (alpha);
			} else {
				state = STATE_FADED;
			}
			break;
		case STATE_NEUTRAL:
		case STATE_FADED:
			setAlpha (0);
			break;
		}
	}


	private void setAlpha(float alpha) {
		setAlpha (alpha, "TextMain");
		setAlpha (alpha, "TextSub");
	}

	private void setAlpha(float alpha, string element) {
		Renderer r = this.gameObject.transform.Find (element).gameObject.GetComponent<Renderer>();


		//Renderer r = GetComponent<Renderer>();
		Color oldColor = r.material.color;
		Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, alpha);          
		r.material.SetColor("_Color", newColor);      

		if (alpha == 0) {
			r.enabled = false;
		} else {
			r.enabled = true;
		}
	}

	public void showMessageFor(float seconds) {
		fadeTime = seconds;
		t = 0;
		state = STATE_FADING_IN;
	}

	public void end() {
		showMessageFor (8);
	}
}
