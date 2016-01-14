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
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setText(string place, string location, string song, string artist) {
		
	}

	private void setText(string element, string text) {
		if (text == null) {
			return;
		}
		TextMesh tm = this.gameObject.transform.Find (element).gameObject.GetComponent<TextMesh>();
		tm.text = text;
	}
}
