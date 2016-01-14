using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GlobeControl : MonoBehaviour {
	public JourneyControl journeyControl;
	public int speed;
	private bool updateGlobe = true;
	public float minGlobeForce;
	public float pinSelectionRange = 2;

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

	CircularSumQueue circularSumQueue = new CircularSumQueue(30);

	private AudioSource audioSource;
	void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}

	bool modeRandom = false;

	float lastGlobeRotation = 0;

	// Update is called once per frame
	void Update () {
		float inputRotation = Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime;

		circularSumQueue.addItem (Mathf.Abs(inputRotation));

		if (updateGlobe) {
			float deltaRotation = Mathf.Abs(transform.rotation.eulerAngles.y - lastGlobeRotation);
            transform.Rotate(0, (speed * inputRotation * (-1)), 0, Space.Self);
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

	public bool isGlobeRotating() {
		if(circularSumQueue.getSum () > minGlobeForce) {
			return true;
		}
		return false;
	}

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

	private void stopAmbientMusic() {
		audioSource.Stop ();
	}

	private void startAmbientMusic() {
		audioSource.Play ();
	}

	public void returnToGlobe() {
		Place currentPlace = JourneySingleton.Instance.getCurrentPlace ();
		journeyControl.setInitial(null);
		updatePin (currentPlace, null);

		updateGlobe = true;


		rotateGlobeToRotation (320);

        JourneySingleton.Instance.setCurrentPlace ((Place)null);
		this.startAmbientMusic ();
	}

	private void rotateGlobeToRotation(float rotation) {
		float rotate = rotation - gameObject.transform.rotation.eulerAngles.y;

		transform.Rotate(0, rotate, 0, Space.Self);
	}

	public void exitGlobe() {
		updateGlobe = false;
		this.stopAmbientMusic ();
	}

	public void turnGlobeRotationOff() {
		updateGlobe = false;
	}

	public void resetPin() {
		Place currentPlace = JourneySingleton.Instance.getCurrentPlace ();
		if (currentPlace != null) {
			GameObject oldPin = GameObject.Find ("Pin_" + currentPlace.getCode ());
			PinControl pinControl = oldPin.GetComponent<PinControl> ();
			pinControl.turnOffPinLight ();
		}
	}
}
