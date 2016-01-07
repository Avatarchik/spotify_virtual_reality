using UnityEngine;
using System.Collections;

public class PlayMovieOnSpace : MonoBehaviour {
	bool isPlaying = false;
	// Use this for initialization
	void Start () {
		setAlpha (0);
	}

	float t = 0;

	float goToAlpha;
	float currentAlpha;

	public const int STATE_NEUTRAL = 0;
	public const int STATE_FADING_IN = 1;
	public const int STATE_FADING_OUT = 2;
	public const int STATE_FADED = 3;

	private int state = STATE_NEUTRAL;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			isPlaying = true;
			fadeIn ();
		}
		else if (Input.GetKeyDown (KeyCode.E)) {
			isPlaying = false;
			fadeOut ();
		} 

		if (state == STATE_FADING_IN || state == STATE_FADING_OUT) {
			float fadeTime = 4;
			if (t < fadeTime) {
				t += Time.deltaTime;
				float alpha = Mathf.Lerp (currentAlpha, goToAlpha, t / fadeTime);
				setAlpha (alpha);
			} else {
				GetComponent<AudioSource> ().Stop ();
				state = STATE_FADED;
			}
		}

		Renderer r = GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;

			

	}

	public void fadeIn() {
		Renderer r = GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;
		movie.Play();
		GetComponent<AudioSource> ().Play ();
		t = 0;
		goToAlpha = 1f;
		currentAlpha = 0;
		state = STATE_FADING_IN;
	}

	public void fadeOut() {
		Renderer r = GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;
		movie.Play();

		t = 0;
		goToAlpha = 0;
		currentAlpha = 1f;
		state = STATE_FADING_OUT;
	}

	public void reset() {
		this.state = STATE_NEUTRAL;
	}

	public int getState() {
		return this.state;
	}

	private void setAlpha(float alpha) {
		Renderer r = GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;

		Color oldColor = r.material.color;
		Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, alpha);          
		r.material.SetColor("_Color", newColor);      

	}
}
