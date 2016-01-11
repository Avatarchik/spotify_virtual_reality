using UnityEngine;
using System.Collections;

public class GlobeControl : MonoBehaviour {
	public JourneyControl journeyControl;
	public int speed;
	private bool updateGlobe = true;

	private AudioSource audioSource;
	void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if (updateGlobe) {
			transform.Rotate (-(Vector3.up * speed * Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime));
	
			Place newPlace = JourneySingleton.Instance.getPlace (gameObject.transform.rotation.eulerAngles.y);
			Place currentPlace = JourneySingleton.Instance.getCurrentPlace ();
			if (currentPlace != newPlace) {
				journeyControl.setInitial(newPlace);
				updatePin (currentPlace, newPlace);
			}
		}
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

		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,320,transform.localEulerAngles.z);

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
