using UnityEngine;
using System.Collections;

public class CameraChanger : MonoBehaviour {
	public Camera publicCamera;

	public OVRCameraRig cameraRift;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
	}

	/**
	 * Change camera position to street view
	 * also change the scale *** Felipes hack ;)
	 */ 
	public void changeToStreetView() {
		//0, 0, -4000
		cameraRift.transform.position = new Vector3(0, 7f, -4000);
		cameraRift.transform.localScale = new Vector3(1, 1, 1);

		publicCamera.enabled = true;
    }

	/**
	 * Change camera position to world globe
	 * also change the scale *** Felipes hack ;)
	 */ 
	public void changeToGlobe() {
		//0, 155, -51
		cameraRift.transform.position = new Vector3(0, 155, -51f);
		cameraRift.transform.localScale = new Vector3(100, 100, 100);
		cameraRift.transform.rotation = Quaternion.Euler(cameraRift.transform.rotation.x, 0, cameraRift.transform.rotation.z);
	
		publicCamera.enabled = false;
	}

	/**
	 * Update camera rotation with the initial street view look
	 */ 
	public void updateCameraRotationStreetView() {
		Place place = JourneySingleton.Instance.getCurrentPlace ();
		cameraRift.transform.rotation = Quaternion.Euler(cameraRift.transform.rotation.x,place.getInitialCameraRotation (),cameraRift.transform.rotation.z);
	}


}
