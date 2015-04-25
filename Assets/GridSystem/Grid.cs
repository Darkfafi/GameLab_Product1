using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public const int GROUND = 0;
	public const int WALL = 1;

	public int width = 14; //21 
	public int height = 8; //12

	public GameObject[,] grid;

	private int[,] gridLevel1 = new int[,]{
		{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
		{2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2},
		{2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2},
		{2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2},
		{2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2},
		{2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2},
		{2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2},
		{2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2},
		{2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2},
		{2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2},
		{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
		{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}
	};

	void Awake(){
		CreateLevel(gridLevel1);
	}

	void CreateLevel(int[,] levelGrid){
		GameObject gridHolder = new GameObject();
		gridHolder.name = "GridHolder";

		//addLevelArt Maybe

		grid = new GameObject[width,height];

		for(int xAxis = 0; xAxis < width; xAxis++){
			for(int yAxis = 0; yAxis < height; yAxis++){

				GameObject gridTile = GetObjectGridSpaceId(levelGrid[yAxis,xAxis]);
				if(gridTile != null){
					gridTile = Instantiate(gridTile)as GameObject;
					gridTile.transform.SetParent(gridHolder.transform);

					gridTile.GetComponent<Tile>().tileGridPosition = new Vector2(xAxis,yAxis);

					gridTile.transform.position = new Vector3(((xAxis * gridTile.GetComponent<Tile>().tileSize.x) + gridTile.GetComponent<Tile>().tileSize.x / 2),
					                                          (yAxis * gridTile.GetComponent<Tile>().tileSize.y) + gridTile.GetComponent<Tile>().tileSize.y / 2,
					                                          gridTile.transform.position.z);
					grid[xAxis,yAxis] = gridTile;
				}
			}
		}
	}

	GameObject GetObjectGridSpaceId(int gridSpaceId){
		//kunnen dus ook ontzichtbare items worden als art er onder zit.
		GameObject gridItem = null;

		switch(gridSpaceId){
			case 0:
				gridItem = Resources.Load("Prefabs/Tiles/TileGround") as GameObject;
			break;
			case 1:
				gridItem = Resources.Load("Prefabs/Tiles/TileWall") as GameObject;
			break;
			case 2:	
				gridItem = null;
			break;
		}

		return gridItem;
	}

	public List<GameObject> GetListPressuredTiles(bool freeTile = false){

		GameObject currentTile;
		List<GameObject> listTiles = new List<GameObject>();

		for(int xAxis = 0; xAxis < width; xAxis++){
			for(int yAxis = 0; yAxis < height; yAxis++){
				currentTile = grid[xAxis,yAxis];
				if(currentTile != null && currentTile.GetComponent<Tile>().typeId != WALL){
					Debug.Log(currentTile.GetComponent<Tile>().pressured);
					if(currentTile.GetComponent<Tile>().pressured){
						if(freeTile){
							if(currentTile.GetComponent<Tile>().containId == 0){
								listTiles.Add(currentTile);
							}
						}else{
							listTiles.Add(currentTile);
						}
					}

				}
			}
		}
		return listTiles;
	}

	public List<GameObject> GetListOfFreeTiles(int? place = null){
		Tile tile;;
		List<GameObject> listOfUseableTiles = new List<GameObject>();
		
		for(int xAxis = 0; xAxis < width; xAxis++){
			for(int yAxis = 0; yAxis < height; yAxis++){
				if(grid[xAxis,yAxis] != null){
					tile = grid[xAxis,yAxis].GetComponent<Tile>();
					if(tile.containId == 0){ //if tile does not already contain a trap 
						if(tile.typeId == place || place == null){ //if it is equal to the asked place like Wall or Ground etc or not asked for place then all free spaces
							
							listOfUseableTiles.Add(tile.gameObject);
							
						}
					}
				}
			}
		}
		return listOfUseableTiles;
	}
}
