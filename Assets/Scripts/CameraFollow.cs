using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Camera.main.transform.position;
		this.transform.rotation = Camera.main.transform.rotation;
	}
}
