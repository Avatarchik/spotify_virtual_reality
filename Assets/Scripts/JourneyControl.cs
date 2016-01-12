using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class JourneyControl : BaseMachine {
	private AudioControl audioControl;
	private PlaceControl placeControl;
	public GlobeControl globeControl;
	public CameraChanger cameraChanger;
	public PlayMovieOnSpace movieControlGlobe;
	public JourneyEndControl journeyEndControl;
	public PlaceTextControl placeTextControl;


	private float timeWhenJourneyStarts;

	int journeyCount = 0;

	const int MAX_PLACES = 1;
	private string []journeyPlaces = new string[MAX_PLACES];

	private const string STATE_NAVIGATING = "STATE_NAVIGATING";
	private const string STATE_PRE_SELECTED = "STATE_PRE_SELECTED";
	private const string STATE_SELECTED_NOISE = "STATE_SELECTED_NOISE";
	private const string STATE_SELECTED = "STATE_SELECTED";
	private const string STATE_JOURNEY_START = "STATE_JOURNEY_START";
	private const string STATE_JOURNEY = "STATE_JOURNEY";
	private const string STATE_PREPARE_TO_NEXT_PLACE = "STATE_PREPARE_TO_NEXT_PLACE";
	private const string STATE_PREPARE_TO_RETURN_TO_GLOBE = "STATE_PREPARE_TO_RETURN_TO_GLOBE";
	private const string STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS = "STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS";

	public JourneyControl(): base(true) {

	}

	// Use this for initialization
	void Start () {
		this.audioControl = GetComponent<AudioControl> ();
		this.placeControl = GetComponent<PlaceControl> ();
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
        

		if (Input.GetKeyUp(KeyCode.B)){
            Debug.Log("VR Recenter");
            InputTracking.Recenter();
        }


		switch (this.getState ()) {
		case STATE_PRE_SELECTED:
			if (audioControl.getState() == STATE_INITIAL) {
				PinControl pinControl = JourneySingleton.Instance.getCurrentPin ();
				pinControl.makePinShine ();
				this.state = STATE_SELECTED_NOISE;
			}
			break;
		case STATE_SELECTED_NOISE:
			PinControl pinControl1 = JourneySingleton.Instance.getCurrentPin ();
			if (pinControl1.isPinShinning () == false) {
				globeControl.turnGlobeRotationOff ();
				globeControl.resetPin ();
				movieControlGlobe.fadeIn ();
				this.state = STATE_SELECTED;
			}
			break;
		case STATE_SELECTED:
			if (movieControlGlobe.getState () == PlayMovieOnSpace.STATE_FADED) {
				this.startJourney ();
				randomizeNext ();
				cameraChanger.changeCamera ();
				this.state = STATE_JOURNEY_START;
			}
			break;
		case STATE_JOURNEY_START:
			movieControlGlobe.fadeOut ();
			placeControl.fadeIn ();
			this.state = STATE_JOURNEY;
			break;
		case STATE_PREPARE_TO_NEXT_PLACE:
			if (movieControlGlobe.getState () == PlayMovieOnSpace.STATE_FADED) {
				setJourneyPlace (JourneySingleton.Instance.getPlace (journeyPlaces [journeyCount]));
				startJourney ();
				this.state = STATE_JOURNEY_START;
			}
			break;
		case STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS:
			if (movieControlGlobe.getState () == PlayMovieOnSpace.STATE_FADED) {
				goToGlobe ();
				movieControlGlobe.fadeOut ();
				journeyEndControl.end ();
			}
			break;
		case STATE_PREPARE_TO_RETURN_TO_GLOBE:
			if (movieControlGlobe.getState () == PlayMovieOnSpace.STATE_FADED) {
				goToGlobe ();
				movieControlGlobe.fadeOut ();
			}
			break;
		case STATE_JOURNEY:
			if (audioControl.audioIsFinishing () || Input.GetKeyUp (KeyCode.N)) {
				journeyCount++;
				if (journeyCount < MAX_PLACES) {
					movieControlGlobe.fadeIn ();
					audioControl.fadeOut ();
					this.state = STATE_PREPARE_TO_NEXT_PLACE;
				} else {
					this.state = STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS;
					prepareToGoToGlobe ();

				}
			}
			if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
				this.state = STATE_PREPARE_TO_RETURN_TO_GLOBE;
				prepareToGoToGlobe ();

			}
			if (Input.GetKeyUp (KeyCode.Return)) {
				goToGlobe ();
			}
			break;
		}
	}

	public void prepareToGoToGlobe() {
		movieControlGlobe.fadeIn ();
	}

	public void goToGlobe() {
		globeControl.returnToGlobe ();
		audioControl.stop ();
		cameraChanger.changeCamera ();
		this.state = STATE_NAVIGATING;
	}

	public void setInitial(Place place) {
		journeyCount = 0;
		if (place == null) {
			this.state = STATE_NAVIGATING;
			audioControl.stop ();
			JourneySingleton.Instance.setCurrentPlace (place);
			return;
		}
		this.state = STATE_PRE_SELECTED;
		setJourneyPlace (place);
	}

	public void randomizeNext() {
		journeyPlaces [0] = JourneySingleton.Instance.getCurrentPlace ().getCode ();

		for (int i = 1; i < MAX_PLACES; i++) {
			addNewRandomizedPlace (i);
		}
	}

	private void addNewRandomizedPlace(int index) {
		bool isPlaceOk = false;
		Place place = null;
		while (isPlaceOk == false) {
			place = JourneySingleton.Instance.getRandomPlace ();
			isPlaceOk = true;
			for (int i = 0; i <= index; i++) {
				if (journeyPlaces [i].CompareTo(place.getCode()) == 0) {
					isPlaceOk = false;
					break;
				}
			}
		}
		journeyPlaces [index] = place.getCode ();
	}



	private void setJourneyPlace(Place place) {
		JourneySingleton.Instance.setCurrentPlace (place);

		// start the audio of the selected index
		this.audioControl.playAudio();
		this.placeControl.setPlace (place.getCode());

		placeTextControl.setText (place.getName (), place.getLocation (), place.getSongTitle (), place.getSongArtist ());
	}

	public void startJourney() {
		cameraChanger.updateCameraRotationStreetView ();
		globeControl.exitGlobe ();
		this.placeControl.applyMaterial ();
		this.audioControl.playFullAudio ();
	}
}
