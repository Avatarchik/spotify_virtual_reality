using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JourneySingleton : Singleton<JourneySingleton>  {
	Dictionary<string, Place> placeHashTable = new Dictionary<string, Place> ();

	private Place currentPlace;
	private PinControl currentPin;

	protected JourneySingleton() {
		addPlace ("Champion_Island", 0, 225);
		addPlace ("Isla_Mujeres", 3, 199);
		addPlace ("Times", 14, 325);
		addPlace ("Salar", 30, 270);

		addPlace ("Iceberg", 40, 200);
		addPlace ("Geleira", 45, 230);

		addPlace ("Natal_Arte", 55, 270, 30, "Natal", "Natal", "Song", "Artist");
		addPlace ("Islandia", 78, 190); //-- music missing
		addPlace ("Castelo", 85, 260);
		addPlace ("Veneza", 97, 180);
		addPlace ("Baby_Calf", 98, 45);
		addPlace ("Pine_Lined_Road", 99, 260);
		addPlace ("Grecia", 101, 0);
		addPlace ("Northern_Lights", 102, 180);
		addPlace ("Egito", 116, 335);
		addPlace ("Elefantinhos", 122, 190);
		addPlace ("Castle", 150, 0);
		addPlace ("Kaindy", 160, 200);
		addPlace ("India", 173, 280);
		addPlace ("Ta_Prohm", 194, 90);
		addPlace ("Montanhas_Laranjas", 209, 170);
		addPlace ("Takinoue", 220);
		addPlace ("Cachu_Verdao", 230, 180);
		addPlace ("Heron_Island", 240, 180);
		//addPlace ("", 245);

		addPlace ("Praia_Com_Estrelas", 245, 0);

		addPlace ("Pinguins", 250, 245);
		addPlace ("No_Meio_Da_Floresta", 255, 270);

		addPlace ("Tartaruguinha", 267, 145);
		addPlace ("Boiando", 271, 160);

		addPlace ("Volcano_Hawaii", 298, 180);

		addPlace ("Yosemite", 340, 180);
		addPlace ("Bryce_Canyon", 350, 235);



	} // guarantee this will be always a singleton only - can't use the constructor!

	public Place getRandomPlace() {
		int index = (int)Random.Range (0, placeHashTable.Count);
		List<string> keys = new List<string>(placeHashTable.Keys);
		return (Place)placeHashTable [keys[index]];
	}

	private void addPlace(string code, float position, float initialCameraPosition = 0, float songMaxTime = 35, string name = null, string location = null, string songTitle = null, string songArtist = null) {
		Place place = new Place(code, position, initialCameraPosition, songMaxTime, name, location, songTitle, songArtist);
		placeHashTable.Add(place.getCode(), place);
	}

	public Place getPlace(float position, float range) {
		foreach(KeyValuePair<string, Place> pairPlace in placeHashTable)
		{
			Place place = pairPlace.Value;
			float diff = Mathf.Abs (place.getPosition () - position);
			if (diff > 180) {
				diff -= 360;
			}
			if (Mathf.Abs(diff) < range) {
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
