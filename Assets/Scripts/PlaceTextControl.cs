using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceTextControl : MonoBehaviour {
	public Text songName;
	public Text songArtist;
	public Text placeName;
	public Text placeLocation;
	public RawImage image1;
	public RawImage image2;

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);

		setAllAlpha (0);
	}
		
	/**
	 * Set the text
	 */ 
	public void setText(string place, string location, string song, string artist) {
		songName.text = song;
		songArtist.text = artist;
		placeName.text = place;
		placeLocation.text = location;

		setAllAlpha (0);
		state = STATE_FADING_IN;
		t = 0;
	}

	/**
	 * Set the render active
	 */ 
	public void setActive(bool active) {
		this.gameObject.SetActive (active);
		state = STATE_FADING_IN;
		t = 0;
	}


	float t = 0;

	public const int STATE_NEUTRAL = 0;
	public const int STATE_FADING_IN = 1;
	public const int STATE_FADING_OUT = 2;
	public const int STATE_FADED = 3;

	private int state = STATE_NEUTRAL;

	public float fadeInTime = 5;
	public float fadeOutTime = 5;

	// Update is called once per frame
	void Update () {
		
		if (state == STATE_FADING_IN) {
			if (t < fadeInTime) {
				t += Time.deltaTime;
				float alpha = Mathf.Lerp (0, 1, t / fadeInTime);
				setAllAlpha (alpha);

			} else {
				state = STATE_FADING_OUT;
				t = 0;
			}


		}
		else if (state == STATE_FADING_OUT)
		{
			if (t < fadeOutTime)
			{
				t += Time.deltaTime;
				float alpha = Mathf.Lerp(1, 0, t / fadeOutTime);
				setAllAlpha(alpha);


			}
			else {
				state = STATE_FADED;
				t = 0;
			}


		}
	}

	public int getState() {
		return this.state;
	}

	private void setAllAlpha(float alpha) {
		setAlpha(songName, alpha);
		setAlpha(songArtist, alpha);
		setAlpha(placeName, alpha);
		setAlpha(placeLocation, alpha);
		setAlpha(image1, alpha);
		setAlpha(image2, alpha);

	}

	private void setAlpha(MaskableGraphic graphic, float alpha) {
		graphic.color = new Color (graphic.color.r, graphic.color.g, graphic.color.b, alpha);

	}
}
