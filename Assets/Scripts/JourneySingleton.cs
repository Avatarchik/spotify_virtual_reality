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

	public const int SCENE_CHANGE_TIME = 10;

	/**
	 * Default song lenght
	 */
	private float defaultSongTime = 31;

	protected JourneySingleton() {
        addPlace ("Isla_Mujeres", 3, 199, defaultSongTime, "Museo Subacuatico de Arte", "Isla Mujeres, México", "Dream a Little Dream", "Eddie Vedder", "#E0E1E1");
		addPlace ("Times", 14, 325, defaultSongTime, "Times Square", "Nova Iorque, EUA", "NY is killing me", "Gil Scott-Heron & Jamie xx", "#5be9ff", true);
		addPlace ("Salar", 30, 270, defaultSongTime, "Salar de Uyuni", "Potosí, Bolívia", "White", "Frank Ocean, John Mayer", "#FEFFC5");
		addPlace ("Rio", 48, 270, defaultSongTime, "Cristo Redentor", "Rio de Janeiro, Brasil", "Eu só quero ser feliz", "MC Marcinho", "#637100", true);
		addPlace ("Geleira", 45, 230, defaultSongTime, "Geleira de Ilulissat", "Ilulissat, Groenlândia", "Easy", "Son Lux", "#E8DACD");
		addPlace ("Natal_Arte", 55, 270, defaultSongTime, "Natal na Copa", "Natal, Brasil", "Oba, lá vem ela", "Jorge Ben Jor", "#10B0A1", true);
		addPlace ("Islandia", 75, 190, defaultSongTime, "Skógafoss", "Skógar, Islândia", "Elephant Gun", "Beirut", "#808A24");
		addPlace ("Barcelona", 0, 0, defaultSongTime, "Parque Güell", "Barcelona, Espanha", "Barcelona", "Giulia Y Los Tellarini", "#dcb06e");
		addPlace ("Veneza", 97, 180, defaultSongTime, "Canal de Veneza", "Veneza, Itália", "James Bond Theme", "Moby", "#db3469");
		addPlace ("Congo", 105, 0, defaultSongTime, "Parque Nacional Queen Elizabeth", "Fort Portal-Mpondwe Road, Uganda", "African Drums", "african tribal orchestra", "#b19ab1", true);
		addPlace ("Victoria", 0, 0, defaultSongTime, "Victoria Falls", "Livingstone, Zâmbia", "Zombie", "Fela Kuti", "#7c7b30");
		addPlace ("Northern_Lights", 101, 180, defaultSongTime, "Lago Pitkäjärvi", "Espoo, Finlândia", "Runaway", "Aurora", "#31d6af");
		addPlace ("Egito", 119, 335, defaultSongTime, "Pirâmides de Gizé", "Cairo, Egito", "Walk Like an Egyptian", "The Bangles", "#a3580e", true);
		addPlace ("Elefantinhos", 122, 190, defaultSongTime, "Samburu National Reserve", "Samburu, Quênia", "Circle of Life", "Carmen Twillie, Lebo M.", "#A89786");
		addPlace ("Castle", 143, 0, defaultSongTime, "Castelo de Saryazd", "Yazd, Irã", "Bad Girls", "M.I.A", "#AAA5A0", true);
		addPlace ("Kaindy", 152, 200, defaultSongTime, "Lago Kaindy", "Almaty, Cazaquistão", "Into The Wild", "LP", "#358993", true);
		addPlace ("India", 170, 280, defaultSongTime, "Observatório de Man Singh", "Varanasi, Índia", "Mehendi Rachi", "Mumford & Sons, Dharohar Project", "#cc6a1a", true);
		addPlace ("Ta_Prohm", 193, 90, defaultSongTime, "Ta Prohm", "Siem Reap, Camboja", "Entrance", "Washed Out", "#d69335", true);
		addPlace ("China", 0, 0, defaultSongTime, "Taierzhuang", "Shandong, China", "China Girl", "David Bowie", "#fb532c");
		addPlace ("Montanhas_Laranjas", 212, 170, defaultSongTime, "Parque Nacional Karlamilyi", "Perth, Austrália", "Elephant", "Tame Impala", "#F46E3C", true);
		addPlace ("Takinoue", 227, 0, defaultSongTime, "Parque Takinoue", "Hokkaidou , Japão", "Zelda's Lullaby", "Taylor Davis", "#D673B6", true);
		addPlace ("Cachu_Verdao", 230, 180, defaultSongTime, "Wes Beckett Falls", "Tasmânia, Austrália", "Quiet Here", "Lupa J", "#2CAE24");
		addPlace ("Heron_Island", 244, 180, defaultSongTime, "Ilha Heron", "Queensland, Austrália", "Pure Shores", "All Saints", "#5A92C1", true);
		addPlace ("Praia_Com_Estrelas", 245, 0, defaultSongTime, "Ilha de Lord Howe", "Tasmânia, Austrália", "True to myself ", "Ziggy Marley", "#F1FBFE");
		addPlace ("Pinguins", 250, 245, defaultSongTime, "Cape Royds", "Ilha de Ross, Antártida", "I Like To Move It", "Will.I.Am", "#E9E9EB");
		addPlace ("No_Meio_Da_Floresta", 264, 270, defaultSongTime, "Trilha de Kepler", "Fiordland, Nova Zelândia", "Jasmine", "Jai Paul", "#006b4a");
		addPlace ("Boiando", 273, 160, defaultSongTime, "Tafeu Cove", "Samoa Americana, EUA", "I'm Getting Ready", "Michael Kiwanuka", "#209CCF", true);
		addPlace ("Hawaii", 292, 180, defaultSongTime, "Kauai", "Havaí, EUA", "Shine", "Wild Belle", "#ECF3EB "); 
		addPlace ("Yosemite", 330, 180, defaultSongTime, "Parque Nacional de Yosemite", "Califórnia, EUA", "Hello", "Adele ", "#7CA0DC", true);
		addPlace ("Bryce_Canyon", 343, 235, defaultSongTime, "Parque Nacional de Bryce Canyon", "Utah, EUA", "Flume", "Bon Iver", "#ED6938");
		addPlace ("Champion_Island", 359, 225, defaultSongTime, "Ilha Champion", "Galápagos, Equador", "So Good at Being in Trouble", "Unknown Mortal Orchestra", "#1485AF", true);
		addPlace("London", 90, 270, defaultSongTime, "Abbey Road", "Londres, Inglaterra", "Come together", "The Beatles", "#a03e05", true);
        addPlace("Huskies", 0, 270, defaultSongTime, "Iqaluit", "Nunavut, Canadá", "Wake Up", "Arcade Fire", "#1485AF");
		addPlace ("Alemanha", 0, 270, defaultSongTime, "Castelo de Neuschwanstein", "Schwangau, Alemanha", "Cello Suite no. 1 in G Major", "J. S. Bach", "#1485AF");
		addPlace ("Salvador", 0, 180, defaultSongTime, "Elevador Lacerda", "Salvador, Brasil", "Meia Lua Inteira", "Caetano Veloso", "#1485AF");

	} // guarantee this will be always a singleton only - can't use the constructor!

	/**
	 * Add a place to our table
	 */
	private void addPlace(string code, float position, float initialCameraPosition = 0, float songMaxTime = 35, string name = null, string location = null, string songTitle = null, string songArtist = null, string color = null, bool activeOnMap = false) {
		Place place = new Place(code, position, initialCameraPosition, songMaxTime, name, location, songTitle, songArtist, color, activeOnMap);
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
			if (place.isActiveOnMap () == false) {
				continue;
			}
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
			if (place.isActiveOnMap () == false) {
				continue;
			}
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
