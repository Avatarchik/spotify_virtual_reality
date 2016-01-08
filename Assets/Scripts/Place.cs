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


	public Place(string code, float position, string name, string location, string songTitle, string songArtist) {
		init (code, position, name, location, songTitle, songArtist);
	}

	private void init(string code, float position, string name, string location, string songTitle, string songArtist) {
		this.code = code;
		this.position = position;
		this.name = name;
		this.location = location;
		this.songTitle = songTitle;
		this.songArtist = songArtist;

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

}
