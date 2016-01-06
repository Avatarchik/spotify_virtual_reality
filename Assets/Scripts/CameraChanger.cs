using UnityEngine;
using System.Collections;

public class CameraChanger : MonoBehaviour {
	public Camera camera1;
	public Camera camera2;
	// Use this for initialization
	void Start () {
		camera1.enabled = true;
		camera2.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void changeCamera() {
		camera1.enabled = !camera1.enabled;
		camera2.enabled = !camera2.enabled;
	}
}
