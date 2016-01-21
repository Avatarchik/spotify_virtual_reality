using UnityEngine;
using System.Collections;

public class SceneControl : MonoBehaviour {
	public FogControl fogControl;
	public MaterialTransition floor;
	public MaterialTransition globe;

	bool isRed = false;

	float fadeTime = JourneySingleton.SCENE_CHANGE_TIME;
	float t = 0;
	// Use this for initialization
	void Start () {
		t = fadeTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (t < fadeTime) {
			t += Time.deltaTime;
		} else {
			t = 0;

			isRed = !isRed;
			CubeAnimation.changeAllWallsColor (isRed);
			fogControl.changeFog (isRed);
			floor.changeMaterial (isRed);
			globe.changeMaterial (isRed);
		}

	}
}
