using UnityEngine;
using System.Collections;

public class WallColorControl : MonoBehaviour {

	Color fromAlbaedo;
	Color toAlbaedo;

	float fromMetalic;
	float toMetalic;

	float fromSmoothness;
	float toSmoothness;

	Color fromEmission;
	Color toEmission;

	public Color blueAlbaedo;
	public Color redAlbaedo;

	public float blueMetalic;
	public float redMetalic;

	public float blueSmoothness;
	public float redSmoothness;

	public Color blueEmission;
	public Color redEmission;

	MeshRenderer meshRenderer ;

	float t = 0;

	float fadeTime = JourneySingleton.SCENE_CHANGE_TIME;

	bool start = false;

	// Use this for initialization
	void Start () {
		meshRenderer = this.gameObject.GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			meshRenderer.material.SetFloat ("_Smoothness", Mathf.Lerp (fromSmoothness, toSmoothness, t / fadeTime));
			meshRenderer.material.SetColor ("_EmissionColor", Color.Lerp (fromEmission, toEmission, t / fadeTime));
			meshRenderer.material.SetFloat ("_Metallic", Mathf.Lerp (fromMetalic, toMetalic, t / fadeTime));
		}

	}

	public void toBlue() {
		t = 0; 
		//fromAlbaedo;
		toAlbaedo = blueAlbaedo;

		fromMetalic = meshRenderer.material.GetFloat ("_Metallic");
		toMetalic = blueMetalic;

		fromSmoothness = meshRenderer.material.GetFloat ("_Smoothness");
		toSmoothness = blueSmoothness;

		fromEmission = meshRenderer.material.GetColor ("_EmissionColor");
		toEmission = blueEmission;
	}

	public void toRed() {
		t = 0;
		//fromAlbaedo;
		toAlbaedo = redAlbaedo;

		fromMetalic = meshRenderer.material.GetFloat ("_Metallic");
		toMetalic = redMetalic;

		fromSmoothness = meshRenderer.material.GetFloat ("_Smoothness");
		toSmoothness = redSmoothness;

		fromEmission = meshRenderer.material.GetColor ("_EmissionColor");
		toEmission = redEmission;
	}

	public void restart() {
		toRed ();
		start = true;
	}
}
