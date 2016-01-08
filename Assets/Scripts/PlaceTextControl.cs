using UnityEngine;
using System.Collections;

public class PlaceTextControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setText(string place, string location, string song, string artist) {
		setText ("Text_Song_Name", place);
		setText ("Text_Song_Group", location);
		setText ("Text_Location_Sub", song);
		setText ("Text_Location", artist);
	}

	private void setText(string element, string text) {
		if (text == null) {
			return;
		}
		TextMesh tm = this.gameObject.transform.Find (element).gameObject.GetComponent<TextMesh>();
		tm.text = text;
	}
}
