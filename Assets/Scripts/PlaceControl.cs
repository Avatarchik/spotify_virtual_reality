using UnityEngine;
using System.Collections;

public class PlaceControl : MonoBehaviour {
	// current material name
	private string materialName = "";

	// the material of this component
	private Renderer renderer;


	// Use this for initialization
	void Start () {
		this.renderer = GetComponent<Renderer> ();
	}

	// set the index of the place that we want to be
	public void setPlace(string materialName) {
		if (this.materialName.CompareTo(materialName) != 0) {
			this.materialName = materialName;
		}
	}
		
	// apply the texture of the selected place
	public void applyMaterial() {
		Material material = (Material)Resources.Load("Materials/StreetView/" + materialName, typeof(Material));
		renderer.material = material;
	}

	/**
	 * TODO: Fade in the street view?
	 */ 
	public void fadeIn() {

	}
		
}
