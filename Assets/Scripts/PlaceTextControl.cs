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
	
	// Update is called once per frame
	void Update () {
	}

	public void setText(string place, string location, string song, string artist) {
		songName.text = song;
		songArtist.text = artist;
		placeName.text = place;
		placeLocation.text = location;
	}

	private void setText(string element, string text) {
		if (text == null) {
			return;
		}
		TextMesh tm = this.gameObject.transform.Find (element).gameObject.GetComponent<TextMesh>();
		tm.text = text;
	}

	public void setActive(bool active) {
		this.gameObject.SetActive (active);
	}
}
