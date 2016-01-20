using UnityEngine;
using System.Collections;

public class Place {
	/**
	 * Code of this place
	 */ 
	string code;

	/**
	 * Y coordinate of the place position on the world globe
	 */ 
	float position;

	/**
	 * Audio clip, the music, of this place
	 */ 
	AudioClip audioClip;

	/**
	 * The streetview material of this place
	 */ 
	Material material;


	string name;
	string location;

	string songTitle;
	string songArtist;

	/**
	 * The initial street view camera rotation
	 */
	float initialCameraRotation;

	/**
	 * The max lenght of the song
	 */ 
	float songMaxTime;

	/**
	 * The rgb color of the leds
	 */ 
	string color;

	bool activeOnMap; 


	public Place(string code, float position, float initialCameraRotation, float songMaxTime, string name, string location, string songTitle, string songArtist, string color, bool activeOnMap) {
		this.code = code;
		this.position = position;
		this.initialCameraRotation = initialCameraRotation;
		this.songMaxTime = songMaxTime;
		this.name = name;
		this.location = location;
		this.songTitle = songTitle;
		this.songArtist = songArtist;
		this.color = color;
		this.activeOnMap = activeOnMap;
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

	public bool isActiveOnMap() {
		return activeOnMap;
	}
}
