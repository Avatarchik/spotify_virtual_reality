using UnityEngine;
using System.Collections;

public class Place {
	string code;
	float position;
	AudioClip audioClip;
	Material material;

	string name;
	string location;

	string songTitle;
	string songArtist;

	float initialCameraRotation;
	float songMaxTime;

	string color;


	public Place(string code, float position, float initialCameraRotation, float songMaxTime, string name, string location, string songTitle, string songArtist, string color) {
		this.code = code;
		this.position = position;
		this.initialCameraRotation = initialCameraRotation;
		this.songMaxTime = songMaxTime;
		this.name = name;
		this.location = location;
		this.songTitle = songTitle;
		this.songArtist = songArtist;
		this.color = color;
		loadSound ();
		loadMaterial ();
	}

	public string getCode() {
		return this.code;
	}

	public float getPosition() {
		return this.position;
	}

	public void loadSound() {
		this.audioClip = (AudioClip)Resources.Load("Audio/Songs/" + code, typeof(AudioClip));
	}

	public void loadMaterial() {
		
	}

	public AudioClip getSound() {
		return this.audioClip;
	}

	public string getName() {
		return name;
	}

	public string getLocation() {
		return location;
	}

	public string getSongTitle() {
		return songTitle;
	}

	public string getSongArtist() {
		return songArtist;
	}

	public float getInitialCameraRotation() {
		return initialCameraRotation;
	}

	public float getSongMaxTime() {
		return songMaxTime;
	}

	public string getColor() {
		return color;
	}
}
