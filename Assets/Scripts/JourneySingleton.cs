using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JourneySingleton : Singleton<JourneySingleton>  {
	Dictionary<string, Place> placeHashTable = new Dictionary<string, Place> ();

	private Place currentPlace;
	private PinControl currentPin;

	protected JourneySingleton() {
		addPlace ("Champion_Island", 0);
		addPlace ("Isla_Mujeres", 3);
		addPlace ("Times", 14);
		addPlace ("Salar", 30);
		addPlace ("Natal_Arte", 55);
		addPlace ("Islandia", 78);
		addPlace ("Castelo", 85);
		addPlace ("Veneza", 97);
		addPlace ("Baby_Calf", 98);
		addPlace ("Pine_Lined_Road", 99);
		addPlace ("Grecia", 101);
		addPlace ("Northern_Lights", 102);
		addPlace ("Egito", 116);
		addPlace ("Elefantinhos", 122);
		addPlace ("Kaindy", 140);
		addPlace ("Ta Prohm", 194);

		addPlace ("Yosemite", 340);
		addPlace ("Bryce_Canyon", 350);
	} // guarantee this will be always a singleton only - can't use the constructor!

	public Place getRandomPlace() {
		int index = (int)Random.Range (0, placeHashTable.Count);
		List<string> keys = new List<string>(placeHashTable.Keys);
		return (Place)placeHashTable [keys[index]];
	}

	private void addPlace(string name, float position) {
		Place place = new Place(name, position);
		placeHashTable.Add(place.getName(), place);
	}

	public Place getPlace(float position) {
		foreach(KeyValuePair<string, Place> pairPlace in placeHashTable)
		{
			Place place = pairPlace.Value;
			if (Mathf.Abs (position - place.getPosition ()) < 2) {
				return place;
			}
		}
		return null;
	}

	public Place getPlace(string name) {
		if (placeHashTable.ContainsKey (name)) {
			return (Place) placeHashTable [name];
		}
		return null;
	}

	public void setCurrentPlace(string name) {
		if (placeHashTable.ContainsKey (name)) {
			setCurrentPlace((Place)placeHashTable [name]);
		} else {
			setCurrentPlace((Place)null);
		}
	}

	public void setCurrentPlace(Place place) {
		this.currentPlace = place;
		//Debug.Log ("Current Place: " + place == null? "Sem place" : place.getName ());
	}

	public Place getCurrentPlace() {
		return this.currentPlace;
	}

	public void setCurrentPin(PinControl pin) {
		this.currentPin = pin;
	}

	public PinControl getCurrentPin() {
		return this.currentPin;
	}
}
