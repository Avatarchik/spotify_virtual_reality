using UnityEngine;
using System.Collections;

public class JourneyControl : MonoBehaviour {
	private AudioControl audioControl;
	private PlaceControl placeControl;
	public GlobeControl globeControl;
	public CameraChanger cameraChanger;

	private float timeWhenSelected = 0;

	// Use this for initialization
	void Start () {
		this.audioControl = GetComponent<AudioControl> ();
		this.placeControl = GetComponent<PlaceControl> ();
	}

	private const int STATE_NAVIGATING = 0;
	private const int STATE_PRE_SELECTED = 2;
	private const int STATE_SELECTED = 3;
	private const int STATE_JOURNEY = 4;

	private int state = STATE_NAVIGATING;

	//string currentJourneyName;

	private float timeWhenJourneyStarts;

	int journeyCount = 0;
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_PRE_SELECTED:
			if (timeWhenSelected != 0 && Time.time > timeWhenSelected + 5.5f) {
				PinControl pinControl = JourneySingleton.Instance.getCurrentPin();
				pinControl.makePinShine ();
				this.state = STATE_SELECTED;
			}
			break;
		case STATE_SELECTED:
			if (timeWhenSelected != 0 && Time.time > timeWhenSelected + 11) {
				timeWhenSelected = 0;
				this.startJourney ();
				//cameraChanger.changeCamera ();
				this.state = STATE_JOURNEY;
				Debug.Log ("cameraChanger.changeCamera");

			}
			break;
		case STATE_JOURNEY:
			if (audioControl.audioFinish ()) {
				journeyCount++;
				if (journeyCount < 2) {
					setJourneyPlace (JourneySingleton.Instance.getPlace("Elefantinho"));
					startJourney ();
				} else {
					goToGlobe ();
				}
			}
			break;
		}
	}

	public void goToGlobe() {
		globeControl.returnToGlobe ();
		cameraChanger.changeCamera ();
		this.state = STATE_NAVIGATING;
	}

	public void setInitial(Place place) {
		if (place == null) {
			this.state = STATE_NAVIGATING;
			audioControl.stop ();
			return;
		}
		this.state = STATE_PRE_SELECTED;
		setJourneyPlace (place);
		timeWhenSelected = Time.time;
	}

	public void randomizeNext() {
	}

	private void setJourneyPlace(Place place) {
		JourneySingleton.Instance.setCurrentPlace (place);

		// start the audio of the selected index
		this.audioControl.playAudio(place.getName());
		this.placeControl.setPlace (place.getName());
	}

	public void startJourney() {
		globeControl.exitGlobe ();
		this.placeControl.applyMaterial ();
		this.audioControl.playFullAudio (JourneySingleton.Instance.getCurrentPlace().getName());
	}
}
