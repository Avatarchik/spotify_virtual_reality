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
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = audios[this.index];
		audio.Play();
	}

	// apply the texture of the selected place
	public void applyMaterial() {
		GetComponent<Renderer> ().material = mats [this.index];
	}

}
