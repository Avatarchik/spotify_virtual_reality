using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JourneySingleton : Singleton<JourneySingleton>  {
	Dictionary<string, Place> placeHashTable = new Dictionary<string, Place> ();

	private Place currentPlace;
	private PinControl currentPin;

	private float defaultTime = 30;

	protected JourneySingleton() {
		addPlace ("Isla_Mujeres", 3, 199, defaultTime, "Catlin Seaview Survey", "Isla Mujeres, México", "Dream a Little Dream", "Eddie Vedder");
		addPlace ("Times", 13, 325, defaultTime, "Times Square", "New York", "empire state of mind", "jayz");
		addPlace ("Salar", 30, 270, defaultTime, "Salar", "", "White", "Frank Ocean- (feat. John Mayer)");
		addPlace ("Iceberg", 40, 200, defaultTime, "Auyuittuq National Park", "", "Wake Up", "arcade");
		addPlace ("Geleira", 45, 230, defaultTime, "", "Groelandia", "Easy", "Son Lux");
		addPlace ("Natal_Arte", 55, 270, defaultTime, "Arte em Natal", "Natal", "Song", "Artist");
		addPlace ("Islandia", 75, 190, defaultTime, "", "Islandia", "Elephant Gun", "Beirut"); //-- music missing
		addPlace ("Castelo", 86, 260, defaultTime, "", "Inglaterra", "Somebody to love", "Queen");
		addPlace ("Veneza", 97, 180, defaultTime, "", "Itália", "James Bond Theme", "Moby");
		addPlace ("Baby_Calf", 98, 45, defaultTime, "", "Noruega", "", "");
		addPlace ("Pine_Lined_Road", 99, 260, defaultTime, "Pine-Lined Road", "Slovakia", "Sunny Road", "Emiliana Torrini");
		addPlace ("Northern_Lights", 101, 180, defaultTime, "Northern Lights", "Rússia", "Aurora", "Runaway");
		addPlace ("Grecia", 102, 0, defaultTime, "", "Grécia", "Hanging on", "Ellie Goulding");
		addPlace ("Egito", 118, 335, defaultTime, "", "Egito", "Walk like an egyptian", "The Bangles");
		addPlace ("Elefantinhos", 122, 190, defaultTime, "", "Kenya", "Circle-of-Life", "");
		addPlace ("Castle", 145, 0, defaultTime, "", "Irão", "", "Bad Girls - M.I.A");
		addPlace ("Kaindy", 152, 200, defaultTime, "", "Cazaquistão", "Into The Wild", "LP - ");
		addPlace ("India", 169, 280, defaultTime, "", "India", "", "Mumford & Sons, Laura Marling, & Dharohar Project - Mehendi Rachi ");
		addPlace ("Ta_Prohm", 194, 90, defaultTime, "", "Camboja", "", "Entrance, washed out (Kripalu Calm)");
		addPlace ("Montanhas_Laranjas", 215, 170, defaultTime, "", "Austrália", "", "Tame Impala - Elephant ");
		addPlace ("Takinoue", 225, 0, defaultTime, "", "Japão", "", "Zelda's Lullaby - Taylor Davis");
		addPlace ("Cachu_Verdao", 230, 180, defaultTime, "", "Tasmânia", "", "Lupa J - Quiet Here ");
		addPlace ("Heron_Island", 240, 180, defaultTime, "", "Austrália", "", "All Saints - Pure Shores");
		addPlace ("Praia_Com_Estrelas", 245, 0, defaultTime, "", "", "True to myself ", "Ziggy Marley ");
		addPlace ("Pinguins", 250, 245, defaultTime, "", "", "I Like To Move It", "Will.I.Am");
		addPlace ("No_Meio_Da_Floresta", 264, 270, defaultTime, "", "Novazelândia", "", "Jasmine - Jai Paul");
		addPlace ("Tartaruguinha", 267, 145, defaultTime, "", "Midway Islands", "Moonrise Swing", "La familia de Ukeleles");
		addPlace ("Boiando", 271, 160, defaultTime, "", "Samoa Americana", "I'm Getting Ready", "Michael Kiwanuka");
		addPlace ("Volcano_Hawaii", 292, 180, defaultTime, "", "Havaí", "", "Kuana Torres Kahele, Napua Greig, James Ford Murphy - Lava ");
		addPlace ("Yosemite", 340, 180, defaultTime, "", "USA", "Hello", "Adele ");
		addPlace ("Bryce_Canyon", 350, 235, defaultTime, "", "USA", "", "Bon Iver - Flume");
		addPlace ("Champion_Island", 358, 225, defaultTime, "", "", "", " So good at being in trouble - Unknown mortal Orchestra");

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

	public Place getNextPlace(float position) {
		foreach(KeyValuePair<string, Place> pairPlace in placeHashTable)
		{
			Place place = pairPlace.Value;
			if (place.getPosition () > position) {
				return place;
			}
		}
		return placeHashTable ["Isla_Mujeres"];
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
