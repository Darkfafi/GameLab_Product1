using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapSpawner : MonoBehaviour {

	Grid grid;

	GameObject[,] levelGrid;

	protected GameSpeedManipulationEffect GameSpeedManipulator;

	//Trap place patterns idee: 
	//dictionary with traps to spawn and time till next trap in the pattern. Counter ++. placeTrap(dictionary[counter]). WaitForNextPlacing(dictionary[counter].time //or something). 
	//if array out of bound. Pattern vorbij laad nieuwe pattern met cooldown wanneer het moet beginnen.
	//(time till next trap. //when is the next pattern) met = new TrapSpawnPattern


	void Awake(){
		GameSpeedManipulator = gameObject.AddComponent<GameSpeedManipulationEffect> ();
	}

	protected bool PlaceTrap(GameObject trap, GameObject tile = null){

		grid = transform.parent.GetComponent<Grid> ();
		levelGrid = grid.grid;

		bool placedTrap = false;

		Tile tileProp;

		if(tile == null){
			List<GameObject> listOfFreeTiles = grid.GetListOfFreeTiles(trap.GetComponent<Trap>().trapPositionType);
			if(listOfFreeTiles.Count != 0){
				tile = listOfFreeTiles[Random.Range(0,listOfFreeTiles.Count)]; // test
			}
		}
		if(tile != null){
			tileProp = tile.GetComponent<Tile> ();

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
			placedTrap = true;
		}
		return placedTrap;
	}
}
