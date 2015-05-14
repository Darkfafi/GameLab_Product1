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

	int patternsPlayedCounter = 0;

	GameObject patternHolder;

	void Start(){
		amountOfGroundTiles = GetComponent<Grid> ().GetListOfTiles (Grid.GROUND,false).Count;
		amountOfWallTiles = GetComponent<Grid> ().GetListOfTiles (Grid.WALL,false).Count;

		FillLevelPaternLists ();
		AddPatternHolder ();

		Invoke("StartArenaTraps",7f); //intro
		GameObject.Find ("StartScreen").GetComponent<FadeInOut> ().FadeAfterTime (3f, 0f);
		GameObject.Find ("StartScreen").GetComponent<FadeInOut> ().OnFadeEnd += DeleteStartScreen;

	}
	void DeleteStartScreen(float value){
		Destroy (GameObject.Find ("StartScreen"));
	}
	void AddPatternHolder(){
		patternHolder = Resources.Load("Prefabs/SpawnPattern") as GameObject;
		patternHolder = Instantiate (patternHolder) as GameObject;
		patternHolder.transform.SetParent (gameObject.transform);

		List<TrapPatternPart> chosenPatternList;
		//float timerTime = GameObject.Find ("Timer").GetComponentInChildren<Timer> ().GetCurrentTimeInSeconds ();
		if(patternsPlayedCounter < 5){
			chosenPatternList = easyPatterns;
		}else if(patternsPlayedCounter < 20){
			chosenPatternList = normalPatterns;
		}else{
			chosenPatternList = hardPatterns;
		}

		patternHolder.GetComponent<TrapSpawnPattern> ().AddPart (chosenPatternList [Random.Range (0, chosenPatternList.Count)]);
		patternsPlayedCounter ++;
	}
	// Update is called once per frame
	void StartArenaTraps () {
		//GameObject.Find ("StartScreen").SetActive (false);
		patternHolder.GetComponent<TrapSpawnPattern>().PlayTrapPattern ();
	}

	void TrapPatternEnded(){
		Destroy (patternHolder);
		AddPatternHolder ();
		StartArenaTraps ();
	}

	void FillLevelPaternLists(){
		TrapPatternPart part = new TrapPatternPart ();


		//----------------------Easy Parts---------------------------

		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP,5f,5);
		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 2f, 5);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 3f, 2);

		easyPatterns.Add (part);

		part = new TrapPatternPart ();

		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 2f, 6);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 2f, 3);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 5f, 3);

		easyPatterns.Add (part);

		part = new TrapPatternPart ();

		part.AddToPart (TrapSpawnPattern.GROUND_POISON_TRAP, 0f, 2);
		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 7f, 8);

		easyPatterns.Add (part);

		part = new TrapPatternPart ();
		//-------------------------------------------------------------

		//----------------------Normal Parts---------------------------

		part.AddToPart (TrapSpawnPattern.GROUND_FIRE_TRAP, 5f, 2);
		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 5f, 6);
		part.AddToPart (TrapSpawnPattern.GROUND_POISON_TRAP, 2f, 1);

		normalPatterns.Add (part);

		part = new TrapPatternPart ();


		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 4f, 8);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 4f, 6);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP_PLAYER_POSITION, 2f, 5);

		normalPatterns.Add (part);

		part = new TrapPatternPart ();
		
		
		part.AddToPart (TrapSpawnPattern.GROUND_FIRE_TRAP, 4f, 4);
		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 5f, 4);
		part.AddToPart (TrapSpawnPattern.GROUND_POISON_TRAP, 2f, 3);
		
		normalPatterns.Add (part);

		part = new TrapPatternPart ();
		
		
		part.AddToPart (TrapSpawnPattern.GROUND_LIGHTNINGSTRIKE_TRAP, 2f, 6);
		part.AddToPart (TrapSpawnPattern.GROUND_POISON_TRAP, 5f, 2);
		part.AddToPart (TrapSpawnPattern.GROUND_LIGHTNINGSTRIKE_TRAP_PLAYER_POSITION, 2f, 6);
		
		normalPatterns.Add (part);
		//-------------------------------------------------------------

		//----------------------Hard Parts---------------------------
		
		part.AddToPart (TrapSpawnPattern.GROUND_POISON_TRAP, 2f, 3);
		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 2f, 10);
		part.AddToPart (TrapSpawnPattern.GROUND_FIRE_TRAP, 10f, 2);
		
		hardPatterns.Add (part);
		
		part = new TrapPatternPart ();
		
		
		part.AddToPart (TrapSpawnPattern.GROUND_LIGHTNINGSTRIKE_TRAP, 2f, 8);
		part.AddToPart (TrapSpawnPattern.WALL_DART_TRAP, 2f, 6);
		part.AddToPart (TrapSpawnPattern.GROUND_LIGHTNINGSTRIKE_TRAP_PLAYER_POSITION, 4f, 8);
		
		hardPatterns.Add (part);
		
		part = new TrapPatternPart ();
		
		
		part.AddToPart (TrapSpawnPattern.GROUND_POISON_TRAP, 1f, 5);
		part.AddToPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 2f, 8);
		part.AddToPart (TrapSpawnPattern.GROUND_FIRE_TRAP, 2f, 2);
		
		hardPatterns.Add (part);
	}


}
