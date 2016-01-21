using UnityEngine;
using System.Collections;

public class MaterialTransition : MonoBehaviour {
	private class FaderMaterial
	{
		float t = 0.0f;
		Renderer r;
		Material materialFrom;
		Material materialTo;
		float fadeTime;
		public FaderMaterial(Renderer r, Material materialFrom, Material materialTo, float fadeTime)
		{
			this.r = r;
			this.materialFrom = materialFrom;
			this.materialTo = materialTo;
			this.fadeTime = fadeTime;
		}

		public bool update()
		{
			if (t < fadeTime)
			{
				t += Time.deltaTime;
				r.material.Lerp(materialFrom, materialTo, t / fadeTime);
				return false;
			}
			return true;
		}
	}

	public Material materialBlue;
	public Material materialRed;

	FaderMaterial faderMaterial;

	float fadeTime = JourneySingleton.SCENE_CHANGE_TIME;

	MeshRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer = this.GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (faderMaterial != null) {
			if (faderMaterial.update ()) {
				faderMaterial = null;
			}
		}
	}

	public void toBlue() {
		faderMaterial = new FaderMaterial (renderer, materialRed, materialBlue, fadeTime);
	}

	public void toRed() {
		faderMaterial = new FaderMaterial (renderer, materialBlue, materialRed, fadeTime);
	}

	public void changeMaterial(bool isRed) {
		if (isRed) {
			toRed ();
		} else {
			toBlue ();
		}
	}
}
