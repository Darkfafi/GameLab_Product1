using UnityEngine;
using System.Collections;

public class TrapSpawnController : MonoBehaviour {

	TrapSpawnPattern testOnePattern;

	void Awake(){
		testOnePattern = gameObject.AddComponent<TrapSpawnPattern> ();
	}

	void Start(){

		testOnePattern.AddPart (TrapSpawnPattern.WALL_DART_TRAP, 1f, 20);
		testOnePattern.AddPart (TrapSpawnPattern.GROUND_SPIKE_TRAP, 1f, 30);
		testOnePattern.AddPart (TrapSpawnPattern.GROUND_SPIKE_TRAP_PLAYER_POSITION, 1f, 30);
		Invoke("StartArenaTraps",2f);

	}
	
	// Update is called once per frame
	void StartArenaTraps () {
		testOnePattern.PlayTrapPattern ();
	}
}
