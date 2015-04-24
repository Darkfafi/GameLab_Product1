using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapSpawner : MonoBehaviour {
	
	public const int GROUND = 0;
	public const int WALL = 1;

	Grid grid;

	GameObject[,] levelGrid;
	GameObject[,] wallTiles;
	GameObject[,] groundTiles;

	// Use this for initialization
	void Start () {

		//alle trapspawners inhereten deze. De game/wave manager regeld wanneer welke word geadd aan de game als component. Als er bijv 5 van deze component is dan switcht het naar de volgende pattern

		grid = GetComponent<Grid>();
		levelGrid = grid.grid;

		List<GameObject> listOfTraps = GetListOfFreeTiles (WALL);
		GameObject targetTile = listOfTraps[Random.Range(0,listOfTraps.Count)]; // test
		PlaceTrap (targetTile);
	}
	
	List<GameObject> GetListOfFreeTiles(int place = 969696){
		Tile tile;

		List<GameObject> listOfUseableTiles = new List<GameObject>();

		for(int xAxis = 0; xAxis < grid.width; xAxis++){
			for(int yAxis = 0; yAxis < grid.height; yAxis++){
				if(levelGrid[xAxis,yAxis] != null){
					tile = levelGrid[xAxis,yAxis].GetComponent<Tile>();
					if(tile.containId == 0){ //if tile does not already contain a trap 
						if(tile.typeId == place || place == 969696){ //if it is equal to the asked place like Wall or Ground etc or not asked for place then all free spaces

							listOfUseableTiles.Add(tile.gameObject);

						}
					}
				}
			}
		}
		return listOfUseableTiles;
	}

	bool PlaceTrap(GameObject tile){ //misschien ook de traptype meegeven of calculeren met wat voor soort tile het is.

		bool placedTrap = false;

		Tile tileProp = tile.GetComponent<Tile> ();

		GameObject trap = new GameObject();
		//plaatsen van trap door middel van te kijken hoeveel grid tiles hij zou innemen. 1 voor 1 te checken of die wel available zijn (als niet break;) if available then build trap. (denk aan age of empire | command and conquer build vlaktes)
		if(tileProp.typeId == WALL){
			trap = Resources.Load("Prefabs/Traps/WallTraps/WallTrapPlaceholder") as GameObject; //liefst een lijst boven aan de code van walltraps waar hij zelf uit kiest.
			//Debug.Log (tileProp.tileGridPosition);
		}else if (tileProp.typeId == GROUND){
			Debug.Log("Ground trap");
		}

		//tileProp.containId = 1; //lieft dat setten met alles wat het aanraakt als he aangemaakt word.
		Quaternion rotationTrap = Quaternion.identity;

		//dit zou netter kunnen door de tile zijn neighbours te laten weten in zich.
		if(levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x),Mathf.CeilToInt(tileProp.tileGridPosition.y) - 1] != null &&
		   levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x),Mathf.CeilToInt(tileProp.tileGridPosition.y) - 1].GetComponent<Tile>().typeId == GROUND){
			//down
			rotationTrap.eulerAngles = new Vector3(0,0,0);
		}else if(levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x) - 1,Mathf.CeilToInt(tileProp.tileGridPosition.y)] != null &&
		         levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x) - 1,Mathf.CeilToInt(tileProp.tileGridPosition.y)].GetComponent<Tile>().typeId == GROUND){
			//left
			rotationTrap.eulerAngles = new Vector3(0,0,270);
		}else if(levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x),Mathf.CeilToInt(tileProp.tileGridPosition.y) + 1] != null && 
		         levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x),Mathf.CeilToInt(tileProp.tileGridPosition.y) + 1].GetComponent<Tile>().typeId == GROUND){
			//up
			rotationTrap.eulerAngles = new Vector3(0,0,180);
		}else if(levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x) + 1,Mathf.CeilToInt(tileProp.tileGridPosition.y)] != null &&
		         levelGrid[Mathf.CeilToInt(tileProp.tileGridPosition.x) + 1,Mathf.CeilToInt(tileProp.tileGridPosition.y)].GetComponent<Tile>().typeId == GROUND){
			//right
			rotationTrap.eulerAngles = new Vector3(0,0,90);
		}

		tileProp.AddTrap(trap,rotationTrap);

		return placedTrap;
	}
}
