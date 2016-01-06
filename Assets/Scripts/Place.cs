using UnityEngine;
using System.Collections;

public class Place {
	string name;
	float position;

	public Place(string name, float position) {
		this.name = name;
		this.position = position;
	}

	public string getName() {
		return name;
	}

	public float getPosition() {
		return position;
	}

}
