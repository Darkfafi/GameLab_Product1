using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TrapSpawnController : MonoBehaviour {

	int amountOfWallTiles;
	int amountOfGroundTiles;
	
	//verschillende lijsten van patterns. Makkelijk, gemiddeld en moeilijk. Aan de hand van je lengte van spelen krijg je steeds meer procent kans op de moeilijkere lijst.

	List<TrapPatternPart> easyPatterns = new List<TrapPatternPart>();
	List<TrapPatternPart> normalPatterns = new List<TrapPatternPart>();
	List<TrapPatternPart> hardPatterns = new List<TrapPatternPart>();

	GameObject patternHolder;

	void Start(){
		amountOfGroundTiles = GetComponent<Grid> ().GetListOfFreeTiles (Grid.GROUND).Count;
		amountOfWallTiles = GetComponent<Grid> ().GetListOfFreeTiles (Grid.WALL).Count;

		FillLevelPaternLists ();
		AddPatternHolder ();

		Invoke("StartArenaTraps",2f); //intro

	}
	void AddPatternHolder(){
		patternHolder = Resources.Load("Prefabs/SpawnPattern") as GameObject;
		patternHolder = Instantiate (patternHolder) as GameObject;
		patternHolder.transform.SetParent (gameObject.transform);

		patternHolder.GetComponent<TrapSpawnPattern> ().AddPart (easyPatterns [Random.Range (0, easyPatterns.Count)]);
		//AddPatternToComponent(patternHolder.GetComponent<TrapSpawnPattern>(),easyPatterns[Random.Range(0,easyPatterns.Count)]);
	}
	// Update is called once per frame
	void StartArenaTraps () {
		patternHolder.GetComponent<TrapSpawnPattern>().PlayTrapPattern ();
	}

	void TrapPatternEnded(){
		Destroy (patternHolder);
		AddPatternHolder ();
		StartArenaTraps ();
	}

	void FillLevelPaternLists(){
		TrapPatternPart part = new TrapPatternPart ();

		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP,5f,5);
		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 5f, 3);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 2f, 5);

		easyPatterns.Add (part);

		part = new TrapPatternPart ();

		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 1f, 8);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 2f, 3);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 3f, 8);

		easyPatterns.Add (part);

	}
}
