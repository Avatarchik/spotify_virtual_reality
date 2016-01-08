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
		//addPlace ("Islandia", 78); -- music missing
		addPlace ("Castelo", 85);
		addPlace ("Veneza", 97);
		addPlace ("Baby_Calf", 98);
		addPlace ("Pine_Lined_Road", 99);
		addPlace ("Grecia", 101);
		addPlace ("Northern_Lights", 102);
		addPlace ("Egito", 116);
		addPlace ("Elefantinhos", 122);
		addPlace ("Castle", 150);
		addPlace ("Kaindy", 160);
		addPlace ("India", 173);
		addPlace ("Ta_Prohm", 194);
		addPlace ("Montanhas_Laranjas", 209);
		addPlace ("Cachu_Verdao", 230);
		addPlace ("Heron_Island", 240);
		//addPlace ("", 245);

		addPlace ("Praia_Com_Estrelas", 250);
		addPlace ("No_Meio_Da_Floresta", 255);

		addPlace ("Tartaruguinha", 267);
		addPlace ("Boiando", 271);

		addPlace ("Volcano_Hawaii", 298);

		addPlace ("Yosemite", 340);
		addPlace ("Bryce_Canyon", 350);
	} // guarantee this will be always a singleton only - can't use the constructor!

	public Place getRandomPlace() {
		int index = (int)Random.Range (0, placeHashTable.Count);
		List<string> keys = new List<string>(placeHashTable.Keys);
		return (Place)placeHashTable [keys[index]];
	}

	private void addPlace(string code, float position) {
		Place place = new Place(code, position);
		placeHashTable.Add(place.getCode(), place);
	}

	public Place getPlace(float position) {
		foreach(KeyValuePair<string, Place> pairPlace in placeHashTable)
		{
			Place place = pairPlace.Value;
			float diff = Mathf.Abs (place.getPosition () - position);
			if (diff > 180) {
				diff -= 360;
			}
			if (Mathf.Abs(diff) < 2) {
				return place;
			}
		}
		return null;
	}

	public Place getPlace(string code) {
		if (placeHashTable.ContainsKey (code)) {
			return (Place) placeHashTable [code];
		}
		return null;
	}

	public void setCurrentPlace(string code) {
		if (placeHashTable.ContainsKey (code)) {
			setCurrentPlace((Place)placeHashTable [code]);
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
