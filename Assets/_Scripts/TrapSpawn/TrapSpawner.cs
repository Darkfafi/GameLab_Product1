using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapSpawner : MonoBehaviour {

	Grid grid;

	GameObject[,] levelGrid;

	GameSpeedManipulationEffect GameSpeedManipulator;

	//Trap place patterns idee: 
	//dictionary with traps to spawn and time till next trap in the pattern. Counter ++. placeTrap(dictionary[counter]). WaitForNextPlacing(dictionary[counter].time //or something). 
	//if array out of bound. Pattern vorbij laad nieuwe pattern met cooldown wanneer het moet beginnen.
	//(time till next trap. //when is the next pattern) met = new TrapSpawnPattern


	void Awake(){
		GameSpeedManipulator = gameObject.AddComponent<GameSpeedManipulationEffect> ();
	}

	// Use this for initialization
	void Start () {

		//alle trapspawners inhereten deze. De game/wave manager regeld wanneer welke word geadd aan de game als component. Als er bijv 5 van deze component is dan switcht het naar de volgende pattern
		grid = GetComponent<Grid>();
		levelGrid = grid.grid;
		/*
		List<GameObject> listOfTraps = grid.GetListOfFreeTiles (Grid.WALL);
		GameObject targetTile = listOfTraps[Random.Range(0,listOfTraps.Count)]; // test
		PlaceTrap (targetTile); 
		*/
	}

	protected bool PlaceTrap(GameObject tile){ //misschien ook de traptype meegeven of calculeren met wat voor soort tile het is.

		bool placedTrap = false;

		Tile tileProp = tile.GetComponent<Tile> ();

		GameObject trap = new GameObject();

		//plaatsen van trap door middel van te kijken hoeveel grid tiles hij zou innemen. 1 voor 1 te checken of die wel available zijn (als niet break;) if available then build trap. (denk aan age of empire | command and conquer build vlaktes)
		if(tileProp.typeId == Grid.WALL){
			trap = Resources.Load("Prefabs/Traps/WallTraps/WallDartTrap") as GameObject; //liefst een lijst boven aan de code van walltraps waar hij zelf uit kiest.
			//Debug.Log (tileProp.tileGridPosition);
		}else if (tileProp.typeId == Grid.GROUND){
			trap = Resources.Load("Prefabs/Traps/GroundTraps/SpikeTrap") as GameObject;
		}

		//tileProp.containId = 1; //lieft dat setten met alles wat het aanraakt als he aangemaakt word.
		Quaternion rotationTrap = Quaternion.identity;

		if(tileProp.typeId == Grid.WALL){
			//dit zou netter kunnen door de tile zijn neighbours te laten weten in zich.
			if(levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x),Mathf.CeilToInt(tileProp.tileGridPosition.y) - 1] != null &&
			   levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x),Mathf.CeilToInt(tileProp.tileGridPosition.y) - 1].GetComponent<Tile>().typeId == Grid.GROUND){
				//down
				rotationTrap.eulerAngles = new Vector3(0,0,0);
			}else if(levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x) - 1,Mathf.CeilToInt(tileProp.tileGridPosition.y)] != null &&
			         levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x) - 1,Mathf.CeilToInt(tileProp.tileGridPosition.y)].GetComponent<Tile>().typeId == Grid.GROUND){
				//left
				rotationTrap.eulerAngles = new Vector3(0,0,270);
			}else if(levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x),Mathf.CeilToInt(tileProp.tileGridPosition.y) + 1] != null && 
			         levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x),Mathf.CeilToInt(tileProp.tileGridPosition.y) + 1].GetComponent<Tile>().typeId == Grid.GROUND){
				//up
				rotationTrap.eulerAngles = new Vector3(0,0,180);
			}else if(levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x) + 1,Mathf.CeilToInt(tileProp.tileGridPosition.y)] != null &&
			         levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x) + 1,Mathf.CeilToInt(tileProp.tileGridPosition.y)].GetComponent<Tile>().typeId == Grid.GROUND){
				//right
				rotationTrap.eulerAngles = new Vector3(0,0,90);
			}
		}
		tileProp.AddTrap(trap,rotationTrap);

		return placedTrap;
	}
}
