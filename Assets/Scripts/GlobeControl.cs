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
			transform.Rotate (Vector3.up * speed * Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime);
	
			Place place = JourneySingleton.Instance.getPlace (gameObject.transform.rotation.eulerAngles.y);
			if (JourneySingleton.Instance.getCurrentPlace() != place) {
				journeyControl.setInitial(place);
				updatePin (JourneySingleton.Instance.getCurrentPlace(), place);
			}
		}
	}

	private void updatePin(Place oldPlace, Place currentPlace) {
		if (oldPlace != null) {
			GameObject oldPin = GameObject.Find ("Pin_" + oldPlace.getName());
			PinControl pinControl = oldPin.GetComponent<PinControl> ();
			pinControl.turnOffPinLight ();
		}

		if (currentPlace != null) {
			GameObject oldPin = GameObject.Find ("Pin_" + currentPlace.getName());
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
		updateGlobe = true;
		this.startAmbientMusic ();
	}

	public void exitGlobe() {
		updateGlobe = false;
		this.stopAmbientMusic ();
	}
}
