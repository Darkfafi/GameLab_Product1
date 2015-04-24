using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

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
					//gridTile.transform.localScale = new Vector3(0.42f,0.413f);

					gridTile.transform.position = new Vector3(((xAxis * gridTile.collider2D.bounds.size.x) + gridTile.collider2D.bounds.size.x / 2),
					                                          (yAxis * gridTile.collider2D.bounds.size.y) + gridTile.collider2D.bounds.size.y / 2,
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
}
