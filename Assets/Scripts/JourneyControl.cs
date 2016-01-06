using UnityEngine;
using System.Collections;

public class JourneyControl : MonoBehaviour {
	private AudioControl audioControl;
	private PlaceControl placeControl;
	private GlobeControl globeControl;
	public CameraChanger cameraChanger;

	private float timeWhenSelected = 0;

	// Use this for initialization
	void Start () {
		this.audioControl = GetComponent<AudioControl> ();
		this.placeControl = GetComponent<PlaceControl> ();
	}

	private const int STATE_NAVIGATING = 0;
	private const int STATE_PRE_SELECTED = 1;
	private const int STATE_JOURNEY = 2;

	private int state = STATE_NAVIGATING;

	string currentJourneyName;

	private float timeWhenJourneyStarts;

	int journeyCount = 0;
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_NAVIGATING:
			if (timeWhenSelected != 0 && Time.time > timeWhenSelected + 5.5f) {
				GameObject Pin_1 = GameObject.Find ("Pin_1");
				PinControl pinControl = Pin_1.GetComponent<PinControl> ();
				pinControl.makePinShine ();
				this.state = STATE_PRE_SELECTED;
			}
			break;
		case STATE_PRE_SELECTED:
			if (timeWhenSelected != 0 && Time.time > timeWhenSelected + 11) {
				timeWhenSelected = 0;
				this.startJourney ();
				cameraChanger.changeCamera ();
				this.state = STATE_JOURNEY;
				Debug.Log ("cameraChanger.changeCamera");

			}
			break;
		case STATE_JOURNEY:
			if (audioControl.audioFinish ()) {
				journeyCount++;
				if (journeyCount < 2) {
					setJourneyPlace ("Iceberg");
					startJourney ();
				} else {
					goToGlobe ();
				}
			}
			break;
		}
	}

	public void goToGlobe() {
		globeControl.startAmbientMusic ();
		this.state = STATE_NAVIGATING;
	}

	public void setInitial(string name) {
		
		this.state = STATE_NAVIGATING;
			
		setJourneyPlace (name);

		timeWhenSelected = Time.time;
	}

	public void randomizeNext() {
	}

	private void setJourneyPlace(string name) {
		currentJourneyName = name;

		// start the audio of the selected index
		this.audioControl.playAudio(currentJourneyName);
		this.placeControl.setPlace (currentJourneyName);
	}

	public void startJourney() {
		globeControl.stopAmbientMusic ();
		this.placeControl.applyMaterial ();
		this.audioControl.playFullAudio (currentJourneyName);
	}
}
