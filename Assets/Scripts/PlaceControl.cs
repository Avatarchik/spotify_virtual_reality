using UnityEngine;
using System.Collections;

public class PlaceControl : MonoBehaviour {
	// current material index
	private int index = 0;

	// the material of this component
	private Material material;

	private AudioControl audioControl;

	Fader fader;

	private float timeToMoveOn = 0;

	private bool changeCamera = false;

	// Use this for initialization
	void Start () {
		this.material = GetComponent<Renderer> ().material;
		this.audioControl = GetComponent<AudioControl> ();

		this.applyMaterial ();
		this.playAudio ();
	}

	void Update() {
		if (fader != null) {
			if (fader.update ()) {
				fader = null;
			}
		}
		/*if (index == 3) {
			if (Time.time > timeToMoveOn - 4) {
				GameObject Pin_1 = GameObject.Find ("Pin_1_Light");
				Light[] lights = Pin_1.GetComponents<Light> ();
				Light light = lights [0];
				fader = new Fader (light, 0.5f, 1.5f, 4);
			}
			
		}*/

		if (timeToMoveOn != 0 && Time.time > timeToMoveOn) {
			changeCamera = true;
			timeToMoveOn = 0;
		}
	}

	public bool shouldChangeCamera() {
		if (changeCamera) {
			changeCamera = false;
			return true;
		}
		return false;
	}

	public void startCountdown() {
	}
		
	// set the index of the place that we want to be
	public void setPlace(int index) {
		if (this.index != index) {

			timeToMoveOn = Time.time + 5;
			this.index = index;

			if (index == 3) {
				turnOnPinLight ();
			} else {
				turnOffPinLight ();
			}



		}


	}



	// start the audio of the selected index
	public void playAudio() {
		AudioClip audioClip = (AudioClip)Resources.Load("Audio/" + getMaterialName(this.index), typeof(AudioClip));
		audioControl.playAudio (audioClip);
	}

	private void turnOnPinLight() {
		GameObject Pin_1 = GameObject.Find("Pin_1_Light");
		Light[] lights = Pin_1.GetComponents<Light>();
		Light light = lights [0];
		fader = new Fader (light, 0, 0.5f, 0.3f);

	}

	private void turnOffPinLight() {
		GameObject Pin_1 = GameObject.Find("Pin_1_Light");
		Light[] lights = Pin_1.GetComponents<Light>();
		Light light = lights [0];

		fader = new Fader (light, 0.5f, 0, 0.3f);

	}

	private class Fader {
		float t = 0.0f;
		Light l;
		float fadeStart;
		float fadeEnd; 
		float fadeTime;
		public Fader(Light l, float fadeStart, float fadeEnd, float fadeTime) {
			this.l = l;
			this.fadeEnd = fadeEnd;
			this.fadeStart = fadeStart;
			this.fadeTime = fadeTime;
		}

		public bool update() {
			
			while (t < fadeTime) {
				t += Time.deltaTime;
				l.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
				return false;
			}
			if (l.intensity == 0.5f) {
				updateFader (0.5f, 1.5f, 4);
				return false;
			}
			return true;
		}

		public void updateFader(float fadeStart, float fadeEnd, float fadeTime) {
			t = 0.0f;
			this.fadeEnd = fadeEnd;
			this.fadeStart = fadeStart;
			this.fadeTime = fadeTime;

		}
	}

	private string getMaterialName(int index) {
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

	// apply the texture of the selected place
	public void applyMaterial() {
		Material material = (Material)Resources.Load("Materials/StreetView/" + getMaterialName(this.index), typeof(Material));
		GetComponent<Renderer> ().material = material;
	}

}
