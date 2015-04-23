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
		grid = GetComponent<Grid>();
		levelGrid = grid.grid;

		List<GameObject> listOfTraps = GetListOfFreeTiles ();
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

	void PlaceTrap(GameObject tile){ //misschien ook de traptype meegeven of calculeren met wat voor soort tile het is.
		Tile tileProp = tile.GetComponent<Tile> ();

		GameObject trap = new GameObject();

		if(tileProp.typeId == WALL){
			trap = Resources.Load("Prefabs/Traps/WallTraps/WallTrapPlaceholder") as GameObject; //liefst een random trap ervan
			Debug.Log (tileProp.tileGridPosition);
		}else if (tileProp.typeId == GROUND){
			Debug.Log("Ground trap");
		}

		tileProp.containId = 1; //lieft dat setten met alles wat het aanraakt als he aangemaakt word.
		Instantiate (trap, tile.transform.position, Quaternion.identity);

	}
}
