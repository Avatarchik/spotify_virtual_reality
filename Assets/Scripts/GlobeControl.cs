using UnityEngine;
using System.Collections;

public class GlobeControl : MonoBehaviour {
	public PlaceControl placeControl;
	public int speed;

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * speed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime);
		/*if (gameObject.transform.rotation.eulerAngles.y > 0 && gameObject.transform.rotation.eulerAngles.y < 180) {
			placeControl.setPlace (0);
			placeControl.playAudio ();
		} else {
			placeControl.setPlace (1);
			placeControl.playAudio ();
		}*/


		int index = (int)gameObject.transform.rotation.eulerAngles.y / 10;
		placeControl.setPlace (index);
		placeControl.playAudio ();
	}
}
