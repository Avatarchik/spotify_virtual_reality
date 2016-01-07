using UnityEngine;
using System.Collections;

public class JourneyControl : MonoBehaviour {
	private AudioControl audioControl;
	private PlaceControl placeControl;
	public GlobeControl globeControl;
	public CameraChanger cameraChanger;
	public PlayMovieOnSpace movieControlGlobe;
	public PlayMovieOnSpace movieControlStreetView;

	private float timeWhenSelected = 0;

	// Use this for initialization
	void Start () {
		this.audioControl = GetComponent<AudioControl> ();
		this.placeControl = GetComponent<PlaceControl> ();
	}

	private const int STATE_NAVIGATING = 0;
	private const int STATE_PRE_SELECTED = 2;
	private const int STATE_SELECTED_NOISE = 3;
	private const int STATE_SELECTED = 4;
	private const int STATE_JOURNEY_START = 5;
	private const int STATE_JOURNEY = 6;



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


				this.state = STATE_SELECTED_NOISE;
			}
			break;
		case STATE_SELECTED_NOISE:
			if (Time.time > timeWhenSelected + 9f) {
				if (movieControlGlobe.getState () == PlayMovieOnSpace.STATE_NEUTRAL) {
					movieControlGlobe.fadeIn ();
				}
				else if(movieControlGlobe.getState() == PlayMovieOnSpace.STATE_FADED) {
					this.state = STATE_SELECTED;
					movieControlGlobe.reset ();
				}
			}
			break;

		case STATE_SELECTED:
			if (timeWhenSelected != 0 && Time.time > timeWhenSelected + 13) {
				timeWhenSelected = 0;
				this.startJourney ();
				cameraChanger.changeCamera ();
				this.state = STATE_JOURNEY_START;
				Debug.Log ("cameraChanger.changeCamera");

			}
			break;
		case STATE_JOURNEY_START:
			movieControlStreetView.fadeOut ();
			placeControl.fadeIn ();
			this.state = STATE_JOURNEY;
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
			if (Input.GetKeyUp (KeyCode.Return)) {
				goToGlobe ();
			}
			break;
		}
	}

	public void goToGlobe() {
		globeControl.returnToGlobe ();
		audioControl.stop ();
		cameraChanger.changeCamera ();
		this.state = STATE_NAVIGATING;
	}

	public void setInitial(Place place) {
		if (place == null) {
			this.state = STATE_NAVIGATING;
			audioControl.stop ();
			JourneySingleton.Instance.setCurrentPlace (place);
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
