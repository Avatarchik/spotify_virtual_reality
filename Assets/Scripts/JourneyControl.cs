using UnityEngine;
using System.Collections;

public class JourneyControl : MonoBehaviour {
	private AudioControl audioControl;
	private PlaceControl placeControl;
	public CameraChanger cameraChanger;

	private float timeWhenSelected = 0;

	// Use this for initialization
	void Start () {
		this.audioControl = GetComponent<AudioControl> ();
		this.placeControl = GetComponent<PlaceControl> ();
	}

	private static int STATE_NAVIGATING = 0;
	private static int STATE_PRE_SELECTED = 1;
	private static int STATE_JOURNEY = 2;

	private int STATE = STATE_NAVIGATING;
	
	// Update is called once per frame
	void Update () {
		if (timeWhenSelected != 0 && Time.time > timeWhenSelected + 10 && this.STATE == STATE_JOURNEY) {
			timeWhenSelected = 0;
			this.startJourney ();
			cameraChanger.changeCamera ();
		}

		if (timeWhenSelected != 0 && Time.time > timeWhenSelected + 5 && this.STATE == STATE_NAVIGATING) {
			GameObject Pin_1 = GameObject.Find ("Pin_1");
			PinControl pinControl = Pin_1.GetComponent<PinControl> ();
			pinControl.makePinShine ();

			this.STATE = STATE_PRE_SELECTED;
		}
	}

	public void setInitial(string name) {
		this.STATE = STATE_NAVIGATING;
			
		// start the audio of the selected index
		this.audioControl.playAudio(name);
		this.placeControl.setPlace (name);

		timeWhenSelected = Time.time;
	}

	public void randomizeNext() {
	}

	public void startJourney() {
		this.placeControl.applyMaterial ();
		randomizeNext ();
	}
}
