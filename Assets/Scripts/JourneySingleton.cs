using UnityEngine;
using System.Collections;

public class JourneySingleton : Singleton<JourneySingleton>  {
	Hashtable placeHashTable = new Hashtable();

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
		addPlace ("Northern_Lights", 130);
		addPlace ("Kaindy", 140);
		addPlace ("Ta Prohm", 194);
	} // guarantee this will be always a singleton only - can't use the constructor!

	private void addPlace(string name, float position) {
		Place place = new Place(name, position);
		placeHashTable.Add(place.getName(), place);
	}

	public Place getPlace(float position) {
		foreach (DictionaryEntry pair in placeHashTable)
		{
			Place place = (Place)pair.Value;
			if (Mathf.Abs (position - place.getPosition ()) < 2) {
				return place;
			}
		}
		return null;
	}

	public Place getPlace(string name) {
		if (placeHashTable.Contains (name)) {
			return (Place) placeHashTable [name];
		}
		return null;
	}

	public void setCurrentPlace(string name) {
		if (placeHashTable.Contains (name)) {
			this.currentPlace = (Place)placeHashTable [name];
		} else {
			this.currentPlace = null;
		}
	}

	public void setCurrentPlace(Place place) {
		this.currentPlace = place;
	}

	public Place getCurrentPlace() {
		return this.currentPlace;
	}
}
