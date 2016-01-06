using UnityEngine;
using System.Collections;

public class Place {
	string name;
	float position;
	AudioClip audioClip;
	Material material;

	public Place(string name, float position) {
		this.name = name;
		this.position = position;

		loadSound ();
		loadMaterial ();
	}

	public string getName() {
		return this.name;
	}

	public float getPosition() {
		return this.position;
	}

	public void loadSound() {
		this.audioClip = (AudioClip)Resources.Load("Audio/Songs/" + name, typeof(AudioClip));
	}

	public void loadMaterial() {
		
	}

	public AudioClip getSound() {
		return this.audioClip;
	}
}
