using UnityEngine;
using System.Collections;

public class PlaceControl : MonoBehaviour {
	public Material[] mats;
	public AudioClip[] audios;

	// current material index
	private int index = 0;

	// the material of this component
	private Material material;

	// the audio source attached to this component
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer> ().material;
		audioSource = GetComponent<AudioSource>();

		this.applyMaterial ();
		this.playAudio ();
	}
		
	// set the index of the place that we want to be
	public void setPlace(int index) {
		if (this.index != index) {
			this.index = index;
		}
	}

	// start the audio of the selected index
	public void playAudio() {
		/*if (audioSource.clip != audios [this.index]) {
			audioSource.clip = audios [this.index];
			audioSource.Play ();
		}*/
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
		//GetComponent<Renderer> ().material = mats [this.index];
	}

}
