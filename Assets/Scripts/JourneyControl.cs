using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class JourneyControl : MonoBehaviour {
	private AudioControl audioControl;
	private PlaceControl placeControl;
	public GlobeControl globeControl;
	public CameraChanger cameraChanger;
	public PlayMovieOnSpace movieControlGlobe;
	public PlayMovieOnSpace movieControlStreetView;
	public JourneyEndControl journeyEndControl;
	public PlaceTextControl placeTextControl;

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
	private const int STATE_PREPARE_TO_NEXT_PLACE = 7;
	private const int STATE_PREPARE_TO_RETURN_TO_GLOBE = 8;
	private const int STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS = 9;

	private int oldState = -1;
	private int state = STATE_NAVIGATING;

	//string currentJourneyName;

	private float timeWhenJourneyStarts;

	int journeyCount = 0;

	const int MAX_PLACES = 3;
	private string []journeyPlaces = new string[MAX_PLACES];

	private float timeStateSelected;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.B))
        {
            Debug.Log("VR Recenter");
            InputTracking.Recenter();
        }

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
				globeControl.turnGlobeRotationOff ();
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

				randomizeNext ();

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
		case STATE_PREPARE_TO_NEXT_PLACE:
			if(Time.time > timeStateSelected + 4.5f) {
				setJourneyPlace (JourneySingleton.Instance.getPlace(journeyPlaces[journeyCount]));
				startJourney ();
				this.state = STATE_JOURNEY_START;
			}
			break;
		case STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS:
			if(Time.time > timeStateSelected + 4.5f) {
				goToGlobe ();
				movieControlGlobe.fadeOut ();
				journeyEndControl.end ();
			}
			break;
		case STATE_PREPARE_TO_RETURN_TO_GLOBE:
			if(Time.time > timeStateSelected + 4.5f) {
				goToGlobe ();
				movieControlGlobe.fadeOut ();
			}
			break;
		case STATE_JOURNEY:
			if (audioControl.audioIsFinish () || Input.GetKeyUp (KeyCode.N)) {
				journeyCount++;
				if (journeyCount < MAX_PLACES) {
					movieControlStreetView.fadeIn ();
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

		if(oldState != state) {
			oldState = state;
			timeStateSelected = Time.time; 
		}
	}

	public void prepareToGoToGlobe() {
		movieControlStreetView.fadeIn ();
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
		timeWhenSelected = Time.time;
	}

	public void randomizeNext() {
		journeyPlaces [0] = JourneySingleton.Instance.getCurrentPlace ().getCode ();

		//TODO: check if the random is the same
		Place place = JourneySingleton.Instance.getRandomPlace ();
		journeyPlaces [1] = place.getCode ();

		place = JourneySingleton.Instance.getRandomPlace ();
		journeyPlaces [2] = place.getCode ();
	}



	private void setJourneyPlace(Place place) {
		JourneySingleton.Instance.setCurrentPlace (place);

		// start the audio of the selected index
		this.audioControl.playAudio(place.getCode());
		this.placeControl.setPlace (place.getCode());

		placeTextControl.setText (place.getName (), place.getLocation (), place.getSongTitle (), place.getSongArtist ());

	}

	public void startJourney() {
		globeControl.exitGlobe ();
		this.placeControl.applyMaterial ();
		this.audioControl.playFullAudio (JourneySingleton.Instance.getCurrentPlace().getCode());
	}
}
