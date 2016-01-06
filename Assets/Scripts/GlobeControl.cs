using UnityEngine;
using System.Collections;

public class GlobeControl : MonoBehaviour {
	public JourneyControl journeyControl;
	public int speed;
	private int index;
	private bool updateGlobe = true;

	private AudioSource audioSource;
	void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if (updateGlobe) {
			transform.Rotate (Vector3.up * speed * Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime);
	

			int index = (int)gameObject.transform.rotation.eulerAngles.y / 10;
			if (this.index != index) {
				journeyControl.setInitial (getLocationName (index));
				updatePin (this.index, index);
				this.index = index;
			}
		}
	}

	private void updatePin(int oldPinIndex, int currentPinIndex) {
		GameObject Pin_1 = GameObject.Find ("Pin_1");
		PinControl pinControl = Pin_1.GetComponent<PinControl> ();
		if (currentPinIndex == 3) {
			pinControl.turnOnPinLight ();
		} else if(oldPinIndex == 3){
			pinControl.turnOffPinLight ();
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

	public static string getLocationName(int index) {
			switch (index) {
			case 0:
				return "Boiando";
			case 1:
				return "Castelo";
			case 2:
				return "Champion_Island";
			case 3:
				return "Egito";
			case 4:
				return "Elefantinhos";
			case 5:
				return "Geleira";
			case 6:
				return "Grecia";
			case 7:
				return "Iceberg";
			case 8:
				return "Isla_Mujeres";
			case 9:
				return "Islandia";
			case 10:
				return "Natal_Arte";
			case 11:
				return "Northern_Lights";
			case 12:
				return "original";
			case 13:
				return "Pine_Lined_Road";
			case 14:
				return "Grecia";
			case 15:
				return "Salar";
			case 16:
				return "Tartaruguinha";
			case 17:
				return "Times";
			case 18:
				return "Veneza";
			case 19:
				return "Volcano_Hawaii";
			case 20:
				return "Yosemite";
			}
			return "";
	}
}
