using UnityEngine;
using System.Collections;

public class CameraChanger : MonoBehaviour {
	public OVRCameraRig camera;

	private bool isCameraOnGlobe = true;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void changeToStreetView() {
		//0, 0, -4000
		camera.transform.position = new Vector3(0, 7f, -4000);
        camera.transform.localScale = new Vector3(1, 1, 1);



    }

	public void updateCameraRotationStreetView() {
		Place place = JourneySingleton.Instance.getCurrentPlace ();
		camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.x,place.getInitialCameraRotation (),camera.transform.rotation.z);
	}

	public void changeToGlobe() {
		//0, 155, -51
		camera.transform.position = new Vector3(0, 155, -51f);
        camera.transform.localScale = new Vector3(100, 100, 100);

    }

	public void changeCamera() {
		if (isCameraOnGlobe) {
			isCameraOnGlobe = false;
			changeToStreetView ();
		} else {
			isCameraOnGlobe = true;
			changeToGlobe ();
		}
	}
}
