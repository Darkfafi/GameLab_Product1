using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapSpawnPattern : TrapSpawner {

	//Random Trap Section	
	//public const string RANDOM_WALLTRAP = "RandomWallTrap";
	//public const string RANDOM_GROUNDTRAP = "RandomGroundTrap";

	//Wall Trap Section
	public const string WALL_DART_TRAP = "WallDartTrap";

	//Ground Trap Section
	public const string GROUND_SPIKE_TRAP = "GroundSpikeTrap";
	public const string GROUND_SPIKE_TRAP_PLAYER_POSITION = "GroundSpikeTrapPlayerPosition";
	public const string GROUND_LIGHTNINGSTRIKE_TRAP = "GroundLightningStrikeTrap";
	public const string GROUND_LIGHTNINGSTRIKE_TRAP_PLAYER_POSITION = "GroundLightningStrikeTrapPlayerPosition";
	public const string GROUND_FIRE_TRAP = "GroundFireTrap";

	//Trap List Maker
	TrapPatternPart _spawnPatternPart;

	//Other
	int _currentTrapPart = 0;
	float _lastSpawnTime = 999999f;

	//for player position placeable traps

	bool playerPositionPlaceAble = false;
	List<GameObject> listOfTrapsToPlace = new List<GameObject>();
	float lastSpawnedPlayerPositionTrap = 999999f;
	int currentTrapOnPlayerPosition = 0;


	public void AddPart(TrapPatternPart spawnPatternPart){
		_spawnPatternPart = spawnPatternPart;
	}

	GameObject GetTrapFromType(string trapType){

		//	GameObject[] trapList;
		GameObject trap = null;

		playerPositionPlaceAble = false;

		switch (trapType) {
		case WALL_DART_TRAP:
			trap = Resources.Load("Prefabs/Traps/WallTraps/WallDartTrap") as GameObject;
				break;
		case GROUND_SPIKE_TRAP:
			trap = Resources.Load("Prefabs/Traps/GroundTraps/SpikeTrap") as GameObject;
			trap.GetComponent<TouchTrap>().triggerOnCreation = false;
				break;
		case GROUND_SPIKE_TRAP_PLAYER_POSITION:
			trap = Resources.Load("Prefabs/Traps/GroundTraps/SpikeTrap") as GameObject;
			trap.GetComponent<TouchTrap>().triggerOnCreation = true;
			playerPositionPlaceAble = true;
			break;
		case GROUND_LIGHTNINGSTRIKE_TRAP:
			trap = Resources.Load("Prefabs/Traps/GroundTraps/LightningStrikeTrap") as GameObject;
			break;
		case GROUND_LIGHTNINGSTRIKE_TRAP_PLAYER_POSITION:
			trap = Resources.Load("Prefabs/Traps/GroundTraps/LightningStrikeTrap") as GameObject;
			playerPositionPlaceAble = true;
			break;
		case GROUND_FIRE_TRAP:
			trap = Resources.Load("Prefabs/Traps/GroundTraps/FireTrap") as GameObject;
			break;

		/*
		 * 
		case RANDOM_GROUNDTRAP:
			trapList = Resources.LoadAll("Prefabs/Traps/GroundTraps") as GameObject[];
			trap = trapList[Random.Range(0,trapList.Length)];
				break;

		case RANDOM_WALLTRAP:
			trapList = Resources.LoadAll("Prefabs/Traps/WallTraps") as GameObject[];
			Debug.Log(trapList);
			trap = trapList[Random.Range(0,trapList.Length)];
				break;*/
		}

		return trap;
	}

	void Update(){
		if(_lastSpawnTime != 999999f && _lastSpawnTime + _spawnPatternPart.timeTillNextSpawn[_currentTrapPart] < GameSpeedManipulator.timeSpendInSeconds){

			_lastSpawnTime = 999999f;
			_currentTrapPart += 1;

			if(_spawnPatternPart.trapType.Count > _currentTrapPart){
				PlayTrapPattern();
			}else{
				SendMessageUpwards("TrapPatternEnded",SendMessageOptions.DontRequireReceiver);
				Debug.Log("End Pattern");
			}
		}
		if(playerPositionPlaceAble){
			if(listOfTrapsToPlace.Count > currentTrapOnPlayerPosition){ 
				if(lastSpawnedPlayerPositionTrap + 0.5f * GetComponent<AllAroundSpeed>().allAroundSpeed < GameSpeedManipulator.timeSpendInSeconds || lastSpawnedPlayerPositionTrap == 999999f){
					PlaceTrap(listOfTrapsToPlace[currentTrapOnPlayerPosition],transform.parent.GetComponent<Grid>().GetListPressuredTiles(listOfTrapsToPlace[currentTrapOnPlayerPosition].GetComponent<Trap>().needsEmptyTile)[0]);

					lastSpawnedPlayerPositionTrap = GameSpeedManipulator.timeSpendInSeconds;
					currentTrapOnPlayerPosition += 1;
				}
			}else{
				currentTrapOnPlayerPosition = 0;
				listOfTrapsToPlace = new List<GameObject>();
				playerPositionPlaceAble = false;
				_lastSpawnTime = GameSpeedManipulator.timeSpendInSeconds;
;			}
		}
	}

	public void PlayTrapPattern(){
		//GameObject tileToPlace = null;
		for(int i = 0; i < _spawnPatternPart.amountOfTrapsInSpawn[_currentTrapPart]; i++){
			listOfTrapsToPlace.Add(GetTrapFromType(_spawnPatternPart.trapType[_currentTrapPart]));
		}
		if(!playerPositionPlaceAble){
			for(int i = 0; i < listOfTrapsToPlace.Count; i++){
				PlaceTrap(listOfTrapsToPlace[i]);
			}
			listOfTrapsToPlace = new List<GameObject>();
			_lastSpawnTime = GameSpeedManipulator.timeSpendInSeconds;
		}

	}
}
