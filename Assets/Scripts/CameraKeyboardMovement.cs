using UnityEngine;
using System.Collections;

public class CameraKeyboardMovement : MonoBehaviour {
	// Camera movement speed
	public float cameraSpeed;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// var pos = transform.position;
		// pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * cameraSpeed;
		// pos.y += Input.GetAxis("Vertical") * Time.deltaTime * cameraSpeed;
		// transform.position = pos;
		var euler = transform.eulerAngles;
		euler.y += Input.GetAxis("Horizontal") * Time.deltaTime * cameraSpeed;
		euler.x += Input.GetAxis("Vertical") * Time.deltaTime * cameraSpeed;
		transform.eulerAngles = euler;
	}
}
