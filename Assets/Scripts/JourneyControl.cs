﻿using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class JourneyControl : BaseMachine {
	private const string STATE_NAVIGATING = "STATE_NAVIGATING";
	private const string STATE_PRE_SELECTED = "STATE_PRE_SELECTED";
	private const string STATE_SELECTED_NOISE = "STATE_SELECTED_NOISE";
	private const string STATE_SELECTED = "STATE_SELECTED";
	private const string STATE_JOURNEY_START = "STATE_JOURNEY_START";
	private const string STATE_JOURNEY = "STATE_JOURNEY";
	private const string STATE_PREPARE_TO_NEXT_PLACE = "STATE_PREPARE_TO_NEXT_PLACE";
	private const string STATE_PREPARE_TO_RETURN_TO_GLOBE = "STATE_PREPARE_TO_RETURN_TO_GLOBE";
	private const string STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS = "STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS";
	private const string STATE_JOURNEY_END = "STATE_JOURNEY_END";
	private const string STATE_JOURNEY_SHOW_END_MESSAGE = "STATE_JOURNEY_SHOW_END_MESSAGE";
	private const string STATE_JOURNEY_PREPARE_TO_START = "STATE_JOURNEY_PREPARE_TO_START";


	private AudioControl audioControl;
	private PlaceControl placeControl;
	public GlobeControl globeControl;
	public CameraChanger cameraChanger;
	public PlayMovieOnSpace movieControlGlobe;
	public JourneyEndControl journeyEndControl;
	public PlaceTextControl placeTextControl;
    public UDPSend udpSend;

    /**
	 * Controls the serial port comunication. Leds rule! 
	 */
    private SerialController serialController;

	/**
	 * Time when the user starts the journey
	 */ 
	private float timeWhenJourneyStarts;

	/**
	 * How many places the user have been
	 */
	int journeyPlacesCount = 0;

	/**
	 * total journey place
	 */ 
	public int totalPlacesOnJourney = 3;

	/**
	 * The journey max time
	 */ 
	public float journeyMaxTime = 3*30;

	/**
	 * If user can change place rotating globe
	 */ 
	public bool canChangePlace = false;

	/**
	 * Number of places to randomize
	 */ 
	const int TOTAL_RANDOM_PLACES = 32;
	private string []journeyPlaces = new string[TOTAL_RANDOM_PLACES];

	public JourneyControl(): base(false) {

	}

	// Use this for initialization
	void Start () {
		this.audioControl = GetComponent<AudioControl> ();
		this.placeControl = GetComponent<PlaceControl> ();
        this.serialController = new SerialController();

        

    }

    private void sendCameraUpdate()
    {
        //udpSend.sendString(UDPPacket.createUpdateCamera(Camera.main.transform.rotation));
        string placeCode = "";
        if(JourneySingleton.Instance.getCurrentPlace() != null)
        {
            placeCode = JourneySingleton.Instance.getCurrentPlace().getCode();

        }

        UDPPacket packet = new UDPPacket(UDPPacket.STREET_VIEW_PACKET, Camera.main.transform.rotation, placeCode);
        udpSend.sendData(packet);
    }

    bool sendUpdateToClient = false;
		
	// Update is called once per frame
	void Update () {
		base.Update ();

		//button values are 0 for left button, 1 for right button, 2 for the middle button.
		if (Input.GetMouseButton (1)) {
			Debug.Log ("VR Recenter");
			InputTracking.Recenter ();
		}

        if (sendUpdateToClient)
        {
            sendCameraUpdate();
        }
        
        switch (this.getState ()) {
		case STATE_PRE_SELECTED:
            if (audioControl.getState() == AudioControl.STATE_FADE_OUT) {
				PinControl pinControl = JourneySingleton.Instance.getCurrentPin ();
				pinControl.makePinShine ();
				this.state = STATE_SELECTED_NOISE;
			}
			break;
		case STATE_SELECTED_NOISE:
			PinControl pinControl1 = JourneySingleton.Instance.getCurrentPin ();
			if (pinControl1.isPinShinning () == false) {
				globeControl.lockGlobeRotation (true);
				globeControl.resetPin ();
				movieControlGlobe.fadeIn ();
				CubeAnimation.changeAllWallsStatus (CubeAnimation.STATE_EXPAND_SPHERIC_MOVE);
				this.state = STATE_SELECTED;

                    UDPPacket packet = new UDPPacket(UDPPacket.PLACE_TRANSITION_PACKET);
                    udpSend.sendData(packet);
                }
			break;
		case STATE_SELECTED:
			if (movieControlGlobe.getState () == PlayMovieOnSpace.STATE_FADED) {
				this.startJourney ();
				randomizeNext ();
				cameraChanger.changeToStreetView ();
				this.state = STATE_JOURNEY_START;
                timeWhenJourneyStarts = Time.time;
            }
			break;
		case STATE_JOURNEY_START:
			movieControlGlobe.fadeOut ();
			placeControl.fadeIn ();
			this.state = STATE_JOURNEY;
			break;
		case STATE_PREPARE_TO_NEXT_PLACE:
			if (movieControlGlobe.getState () == PlayMovieOnSpace.STATE_FADED) {
				setCurrentJourneyPlace (JourneySingleton.Instance.getPlace (journeyPlaces [journeyPlacesCount]));
				startJourney ();
				this.state = STATE_JOURNEY_START;
				CubeAnimation.changeAllWallsStatus (CubeAnimation.STATE_INITIAL_POSITION);
			}
			break;
		case STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS:
			if (movieControlGlobe.getState () == PlayMovieOnSpace.STATE_FADED) {
				goToGlobe ();
				movieControlGlobe.fadeOut ();
				journeyEndControl.end ();

				this.state = STATE_JOURNEY_SHOW_END_MESSAGE;

                CubeAnimation.changeAllWallsStatus(CubeAnimation.STATE_FINAL_POSITION);
            }
			break;
		case STATE_JOURNEY:

			if (audioControl.audioIsFinishing (journeyPlacesCount==0) || (canChangePlace && globeControl.isGlobeRotating() && audioControl.minTimeRespected() == true)) {
                float timeDiff = Time.time - timeWhenJourneyStarts;
				journeyPlacesCount++;
				if (journeyPlacesCount < TOTAL_RANDOM_PLACES && timeDiff < journeyMaxTime && journeyPlacesCount < totalPlacesOnJourney) {
					movieControlGlobe.fadeIn ();
					audioControl.fadeOut ();
					this.state = STATE_PREPARE_TO_NEXT_PLACE;
                        
                    UDPPacket packet = new UDPPacket(UDPPacket.PLACE_TRANSITION_PACKET);
                    udpSend.sendData(packet);
                } else {
					this.state = STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS;
					movieControlGlobe.fadeIn ();
                    
					UDPPacket packet = new UDPPacket(UDPPacket.PLACE_TRANSITION_PACKET);
                    udpSend.sendData(packet);
                }
			}
			if (Input.GetKeyUp (KeyCode.Return)) {
                    this.state = STATE_PREPARE_TO_RETURN_TO_GLOBE_SUCCESS;
                    movieControlGlobe.fadeIn();

                    UDPPacket packet = new UDPPacket(UDPPacket.PLACE_TRANSITION_PACKET);
                    udpSend.sendData(packet);
            }
			break;
		case STATE_JOURNEY_SHOW_END_MESSAGE:
			if (journeyEndControl.getState () == JourneyEndControl.STATE_FADED) {
				RenderControl.setFogToNight();
				this.state = STATE_JOURNEY_END;
			}
			break;
		case STATE_JOURNEY_END:

			/**
	 		* Check if mouse button was clicked and do the correct actions
	 		*/
			//button values are 0 for left button, 1 for right button, 2 for the middle button.
			if (Input.GetMouseButton(1)){
				Debug.Log("VR Recenter");
				InputTracking.Recenter();
				PinControl.setAllPinsVisibility (true);
				globeControl.lockGlobeRotation (false);

				serialController.setColourAll(Utils.toSerialColor(colorInitial));

				journeyEndControl.hideLogo(6);

				CubeAnimation.changeAllWallsStatus (CubeAnimation.STATE_FINAL_POSITION_MOVE);

				this.state = STATE_JOURNEY_PREPARE_TO_START;
			}
			break;

		case STATE_JOURNEY_PREPARE_TO_START:
			

			if (this.getTimeSinceStateWasSelected () > 6) {
				RenderControl.setFogToDay();
				this.state = STATE_NAVIGATING;
			}

			break;
		}
	}
		
    string colorInitial = "00FF00";

    /**
	 * Return to globe
	 */
    private void goToGlobe() {
		globeControl.returnToGlobe ();
		audioControl.stop ();
		cameraChanger.changeToGlobe();
		placeTextControl.setActive (false);


        sendUpdateToClient = false;

        UDPPacket packet = new UDPPacket(UDPPacket.GLOBE_PACKET);
        udpSend.sendData(packet);

        serialController.setColourAll(Utils.toSerialColor(colorInitial));
    }

	/**
	 * Set the current place of our jorney
	 */ 
	private void setCurrentJourneyPlace(Place place) {
		JourneySingleton.Instance.setCurrentPlace (place);

		// start the audio of the selected index
		this.audioControl.playAudio();
		this.placeControl.setPlace (place.getCode());

		placeTextControl.setText (place.getName (), place.getLocation (), place.getSongTitle (), place.getSongArtist ());

        serialController.setColourAll(Utils.toSerialColor(place.getColor()));
    }

	/**
	 * Set the initial place of our jorney
	 */ 
	public void setInitial(Place place) {
		// reset the places counts
		journeyPlacesCount = 0;
		if (place == null) {
			this.state = STATE_NAVIGATING;
			audioControl.stop ();
			JourneySingleton.Instance.setCurrentPlace (place);
			return;
		}
        this.state = STATE_PRE_SELECTED;
		setCurrentJourneyPlace (place);
	}


	/**
	 * Start our jorney 
	 */
	public void startJourney() {
		cameraChanger.updateCameraRotationStreetView ();
		globeControl.exitGlobe ();


        sendUpdateToClient = true;

        placeTextControl.setActive (true);
		this.placeControl.applyMaterial ();
		this.audioControl.playFullAudio ();
	}

	/**
	 * Fill the places of journey user 
	 */ 
	public void randomizeNext() {
		journeyPlaces [0] = JourneySingleton.Instance.getCurrentPlace ().getCode ();
		for (int i = 1; i < TOTAL_RANDOM_PLACES; i++) {
			addNewRandomizedPlace (i);
		}
	}

	private void addNewRandomizedPlace(int index) {
		bool isPlaceOk = false;
		Place place = null;
		while (isPlaceOk == false) {
			place = JourneySingleton.Instance.getRandomPlace ();
			isPlaceOk = true;
			for (int i = 0; i < index; i++) {
				if (journeyPlaces [i].CompareTo(place.getCode()) == 0) {
					isPlaceOk = false;
					break;
				}
			}
		}
		journeyPlaces [index] = place.getCode ();
	}
}