using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JourneySingleton : Singleton<JourneySingleton>  {
	/**
	 * Table with all places and codes
	 */
	Dictionary<string, Place> placeHashTable = new Dictionary<string, Place> ();

	/**
	 * The current active place
	 */
	private Place currentPlace;
	/**
	 * The current active pin
	 */
	private PinControl currentPin;

	/**
	 * Default song lenght
	 */
	private float defaultSongTime = 20;

	protected JourneySingleton() {
		addPlace ("Isla_Mujeres", 3, 199, defaultSongTime, "Museo Subacuatico de Arte", "Isla Mujeres, México", "Dream a Little Dream", "Eddie Vedder", "#E0E1E1");
		addPlace ("Times", 13, 325, defaultSongTime, "Times Square", "Nova Iorque, EUA", "Empire State of Mind", "Jay Z, Alicia Keys", "#5be9ff");
		addPlace ("Salar", 30, 270, defaultSongTime, "Salar de Uyuni", "Bolívia", "White", "Frank Ocean, John Mayer", "#FEFFC5");
		addPlace ("Iceberg", 40, 200, defaultSongTime, "Parque Nacional Auyuittuq", "Nunavut, Canadá", "Wake Up", "Arcade Fire", "#448887");
		addPlace ("Geleira", 45, 230, defaultSongTime, "Geleira de Ilulissat", "Groenlândia", "Easy", "Son Lux", "#E8DACD");
		addPlace ("Natal_Arte", 55, 270, defaultSongTime, "Natal na Copa", "Natal, Brasil", "Eu Quero é Botar Meu Bloco na Rua", "Sérgio Sampaio", "#10B0A1");
		addPlace ("Islandia", 75, 190, defaultSongTime, "Skógafoss", "Islândia", "Elephant Gun", "Beirut", "#808A24"); //-- music missing
		addPlace ("Castelo", 86, 260, defaultSongTime, "Castelo de Raglan", "País de Gales, Reino Unido", "Somebody to Love", "Queen", "#A38D75");
		addPlace ("Veneza", 97, 180, defaultSongTime, "Canal de Veneza", "Veneza, Itália", "James Bond Theme", "Moby", "#db3469");
		addPlace ("Baby_Calf", 98, 45, defaultSongTime, "Östergötland", "Suécia", "Drive On", "Johnny Cash", "#A3A262");
		addPlace ("Pine_Lined_Road", 99, 260, defaultSongTime, "Rodovia 537", "Vysoké Tatry, Eslováquia", "Sunny Road", "Emiliana Torrini", "#4E5D26");
		addPlace ("Northern_Lights", 101, 180, defaultSongTime, "Lago Pitkäjärvi", "Finlândia", "Runaway", "Aurora", "#31d6af");
		addPlace ("Grecia", 102, 0, defaultSongTime, "Ilha de Hydra", "Grécia", "Hanging on", "Ellie Goulding", "#e8b47d");
		addPlace ("Egito", 118, 335, defaultSongTime, "Pirâmides de Gizé", "Cairo, Egito", "Walk Like an Egyptian", "The Bangles", "#a3580e");
		addPlace ("Elefantinhos", 122, 190, defaultSongTime, "Samburu National Reserve", "Quênia", "Circle of Life", "Carmen Twillie, Lebo M.", "#A89786");
		addPlace ("Castle", 145, 0, defaultSongTime, "Castelo de Saryazd", "Irã", "Bad Girls", "M.I.A", "#AAA5A0");
		addPlace ("Kaindy", 152, 200, defaultSongTime, "Lago Kaindy", "Cazaquistão", "Into The Wild", "LP", "#358993");
		addPlace ("India", 169, 280, defaultSongTime, "Man Singh Observatório", "Índia", "Mumford & Sons, Dharohar Project", "Mehendi Rachi", "#cc6a1a");
		addPlace ("Ta_Prohm", 194, 90, defaultSongTime, "Ta Prohm", "Siem Reap, Camboja", "Entrance", "Washed Out", "#d69335");
		addPlace ("Montanhas_Laranjas", 215, 170, defaultSongTime, "Parque Nacional Karlamilyi", "Austrália", "Elephant", "Tame Impala", "#F46E3C");
		addPlace ("Takinoue", 225, 0, defaultSongTime, "Parque Takinoue", "Hokkaidou , Japão", "Zelda's Lullaby", "Taylor Davis", "#D673B6");
		addPlace ("Cachu_Verdao", 230, 180, defaultSongTime, "Wes Beckett Falls", "Tasmânia, Austrália", "Lupa J", "Quiet Here", "#2CAE24");
		addPlace ("Heron_Island", 240, 180, defaultSongTime, "Ilha Heron", "Queensland, Austrália", "Pure Shores", "All Saints", "#5A92C1");
		addPlace ("Praia_Com_Estrelas", 245, 0, defaultSongTime, "Ilha de Lord Howe", "Tasmânia, Austrália", "True to myself ", "Ziggy Marley", "#F1FBFE");
		addPlace ("Pinguins", 250, 245, defaultSongTime, "Cape Royds", "Ilha de Ross, Antártida", "I Like To Move It", "Will.I.Am", "#E9E9EB");
		addPlace ("No_Meio_Da_Floresta", 264, 270, defaultSongTime, "Trilha de Kepler", "Parque Nacional de Fiordland, Nova Zelândia", "Jasmine", "Jai Paul", "#006b4a");
		addPlace ("Tartaruguinha", 267, 145, defaultSongTime, "Atol Pearl e Hermes", "Havaí, EUA", "Moonrise Swing", "La familia de Ukeleles", "#009b67");
		addPlace ("Boiando", 271, 160, defaultSongTime, "Tafeu Cove", "Samoa Americana, EUA", "I'm Getting Ready", "Michael Kiwanuka", "#209CCF");
		addPlace ("Volcano_Hawaii", 292, 180, defaultSongTime, "Vulcão Kilauea", "Havaí, EUA", "Lava", "Kuana Torres Kahele, Napua Greig, James Ford Murphy", "#ECF3EB");
		addPlace ("Yosemite", 340, 180, defaultSongTime, "Parque Nacional de Yosemite", "Califórnia, EUA", "Hello", "Adele ", "#7CA0DC");
		addPlace ("Bryce_Canyon", 350, 235, defaultSongTime, "Parque Nacional de Bryce Canyon", "Utah, EUA", "Flume", "Bon Iver", "#ED6938");
		addPlace ("Champion_Island", 357, 225, defaultSongTime, "Ilha Champion", "Galápagos, Equador", "Unknown Mortal Orchestra", " So Good at Being in Trouble", "#1485AF");

	} // guarantee this will be always a singleton only - can't use the constructor!

	/**
	 * Add a place to our table
	 */
	private void addPlace(string code, float position, float initialCameraPosition = 0, float songMaxTime = 35, string name = null, string location = null, string songTitle = null, string songArtist = null, string color = null) {
		Place place = new Place(code, position, initialCameraPosition, songMaxTime, name, location, songTitle, songArtist, color);
		placeHashTable.Add(place.getCode(), place);
	}

	/**
	 * Return a random place
	 */ 
	public Place getRandomPlace() {
		int index = (int)Random.Range (0, placeHashTable.Count);
		List<string> keys = new List<string>(placeHashTable.Keys);
		return (Place)placeHashTable [keys[index]];
	}


	/**
	 * Get a place based on the current position and a range to find for
	 */ 
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

	/**
	 * Return the next nearest place based on a position
	 */ 
	public Place getNextNearestPlace(float position) {
		foreach(KeyValuePair<string, Place> pairPlace in placeHashTable)
		{
			Place place = pairPlace.Value;
			if (place.getPosition () > position) {
				return place;
			}
		}
		return placeHashTable ["Isla_Mujeres"];
	}

	/**
	 * Get a place base on his code
	 */ 
	public Place getPlace(string code) {
		if (placeHashTable.ContainsKey (code)) {
			return (Place) placeHashTable [code];
		}
		return null;
	}

	/**
	 * Set the current place base on his code
	 */ 
	public void setCurrentPlace(string code) {
		if (placeHashTable.ContainsKey (code)) {
			setCurrentPlace((Place)placeHashTable [code]);
		} else {
			setCurrentPlace((Place)null);
		}
	}

	/**
	 * Set the current place
	 */ 
	public void setCurrentPlace(Place place) {
		this.currentPlace = place;
	}

	/**
	 * Get the current place
	 */ 
	public Place getCurrentPlace() {
		return this.currentPlace;
	}

	/**
	 * Set the current pin
	 */ 
	public void setCurrentPin(PinControl pin) {
		this.currentPin = pin;
	}

	/**
	 * Get the current pin
	 */ 
	public PinControl getCurrentPin() {
		return this.currentPin;
	}
}
