using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GlobeControl : MonoBehaviour {
	private class CircularSumQueue {
		float sum = 0;
		int maxSize;
		Queue<float> queue = new Queue<float>();

		public CircularSumQueue(int maxSize) {
			this.maxSize = maxSize;
		}

		public void addItem(float value) {
			checkAndRemoveItem ();
			queue.Enqueue (value);
			sum += value;
		}

		public void checkAndRemoveItem() {
			if (queue.Count >= maxSize - 1) {
				sum -= queue.Dequeue ();
			}
		}

		public float getSum() {
			return sum;
		}
	}

	private CircularSumQueue circularSumQueue = new CircularSumQueue(30);
	private AudioSource audioSource;
	public JourneyControl journeyControl;

	/**
	 * Globe rotation speed
	 */ 
	public int rotationSpeed;

	/**
	 * If the globe rotation is locked
	 */ 
	private bool isGlobeLocked = false;

	/**
	 * Min globe force to actually change the globe position when the user is on street view
	 */ 
	public float minGlobeForce;

	/**
	 * The degrees to consider if the pin is selected
	 */ 
	public float pinSelectionRange = 2;

	bool modeRandom = false;
	float lastGlobeRotation = 0;


	void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}



	// Update is called once per frame
	void Update () {
		float inputRotation = Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime;

		circularSumQueue.addItem (Mathf.Abs(inputRotation));

		if (isGlobeLocked == false) {
			float deltaRotation = Mathf.Abs(transform.rotation.eulerAngles.y - lastGlobeRotation);
			transform.Rotate(0, (rotationSpeed * inputRotation * (-1)), 0, Space.Self);
			lastGlobeRotation = transform.rotation.eulerAngles.y;
            Place newPlace = JourneySingleton.Instance.getPlace (gameObject.transform.rotation.eulerAngles.y, pinSelectionRange);
			Place currentPlace = JourneySingleton.Instance.getCurrentPlace ();

			/*if (modeRandom == false && circularSumQueue.getSum () > 0.6 && inputRotation != 0 ) {
				Debug.Log ("Mode random: " + circularSumQueue.getSum ());
				modeRandom = true;
			}
			if (modeRandom == true && Mathf.Abs(inputRotation) <= 0 && newPlace == null && deltaRotation == 0) {
				modeRandom = false;
				Debug.Log ("Mode random false ");
				Debug.Log ("1: " + gameObject.transform.rotation.eulerAngles.y);

				Place place = JourneySingleton.Instance.getNextPlace (gameObject.transform.rotation.eulerAngles.y);
				rotateGlobeToRotation (place.getPosition ());
				Debug.Log ("2: " + place.getPosition ());
			}*/

			if (currentPlace != newPlace) {
				journeyControl.setInitial(newPlace);
				updatePin (currentPlace, newPlace);
			}
		}
	}

	/**
	 * Return if the globe is still in movement
	 */
	public bool isGlobeRotating() {
		if(circularSumQueue.getSum () > minGlobeForce) {
			return true;
		}
		return false;
	}

	/**
	 * Update the current selected pin
	 */ 
	private void updatePin(Place oldPlace, Place currentPlace) {
		if (oldPlace != null) {
			GameObject oldPin = GameObject.Find ("Pin_" + oldPlace.getCode());
			PinControl pinControl = oldPin.GetComponent<PinControl> ();
			pinControl.turnOffPinLight ();
		}

		if (currentPlace != null) {
			GameObject oldPin = GameObject.Find ("Pin_" + currentPlace.getCode());
			PinControl pinControl = oldPin.GetComponent<PinControl> ();
			pinControl.turnOnPinLight ();
			JourneySingleton.Instance.setCurrentPin (pinControl);
		} else {
			JourneySingleton.Instance.setCurrentPin (null);
		}
	}

	/**
	 * Stop the ambient musick
	 */
	private void stopAmbientMusic() {
		audioSource.Stop ();
	}

	/**
	 * Start the ambient music
	 */ 
	private void startAmbientMusic() {
		audioSource.Play ();
	}

	/**
	 * Return to globe after a street view journey
	 */ 
	public void returnToGlobe() {
		Place currentPlace = JourneySingleton.Instance.getCurrentPlace ();
		journeyControl.setInitial(null);
		updatePin (currentPlace, null);

		putGlobeOnRotation (0);

        JourneySingleton.Instance.setCurrentPlace ((Place)null);
		this.startAmbientMusic ();

		PinControl.setAllPinsVisibility (false);
	}

	/**
	 * Rotate the globe to the required rotation
	 */ 
	private void rotateGlobeToRotation(float rotation) {
		float rotate = rotation - gameObject.transform.rotation.eulerAngles.y;

		transform.Rotate(0, rotate, 0, Space.Self);
	}

	/**
	 * Rotate the globe to the required rotation
	 */ 
	private void putGlobeOnRotation(float rotation) {
		transform.Rotate(0, rotation, 0, Space.Self);
	}

	/**
	 * Exit the globe to street view
	 */ 
	public void exitGlobe() {
		lockGlobeRotation (true);
		this.stopAmbientMusic ();
	}

	/**
	 * Lock the globe rotation
	 */ 
	public void lockGlobeRotation(bool lockGlobe) {
		isGlobeLocked = lockGlobe;
	}

	/**
	 * Reset current pin
	 */ 
	public void resetPin() {
		Place currentPlace = JourneySingleton.Instance.getCurrentPlace ();
		if (currentPlace != null) {
			GameObject oldPin = GameObject.Find ("Pin_" + currentPlace.getCode ());
			PinControl pinControl = oldPin.GetComponent<PinControl> ();
			pinControl.turnOffPinLight ();
		}
	}
}
