using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceTextControl : MonoBehaviour {
	public Text songName;
	public Text songArtist;
	public Text placeName;
	public Text placeLocation;


	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);
	}
	

	/**
	 * Set the text
	 */ 
	public void setText(string place, string location, string song, string artist) {
		songName.text = song;
		songArtist.text = artist;
		placeName.text = place;
		placeLocation.text = location;
	}

	/**
	 * Set the render active
	 */ 
	public void setActive(bool active) {
		this.gameObject.SetActive (active);
	}
}
