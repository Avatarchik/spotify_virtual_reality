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

	CircularSumQueue circularSumQueue = new CircularSumQueue(10);

	private AudioSource audioSource;
	void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		float inputRotation = Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime;

		circularSumQueue.addItem (Mathf.Abs(inputRotation));

		if (circularSumQueue.getSum () > minGlobeForce) {
			Debug.Log ("circularSumQueue.getSum () = " + circularSumQueue.getSum ());
		}

		if (updateGlobe) {

            transform.Rotate(0, (speed * inputRotation * (-1)), 0, Space.Self);
            //transform.Rotate (-(this.transform.up * speed * inputRotation));
            //transform.Rotate (-(this.transform.up.normalized * speed * inputRotation));
            //transform.rotation = Quaternion.AngleAxis(transform.rotation.y + speed * inputRotation, Vector3.up);
            //transform.Rotate(0, 1, 0);
            //transform.rotation = Quaternion.Euler(0, 90, 0);

            //transform.RotateAroundLocal(Vector3.up, speed * inputRotation);

            Place newPlace = JourneySingleton.Instance.getPlace (gameObject.transform.rotation.eulerAngles.y, pinSelectionRange);
			Place currentPlace = JourneySingleton.Instance.getCurrentPlace ();
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


        float rotate = 320 - gameObject.transform.rotation.eulerAngles.y;

        transform.Rotate(0, rotate, 0, Space.Self);
        //transform.rotation = new Quaternion(transform.rotation.x, 320, transform.rotation.z, transform.rotation.w);

        JourneySingleton.Instance.setCurrentPlace ((Place)null);
		this.startAmbientMusic ();
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
