using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Trap : MonoBehaviour {

	public float lifeTimeInSeconds = 5f;

	public float timeSpendInSeconds = 0f;

	protected AllAroundSpeed allAroundSpeed;

	private GameObject currentTile;

	void Awake(){
		allAroundSpeed = gameObject.AddComponent<AllAroundSpeed> ();
		gameObject.AddComponent<GameSpeedManipulationEffect> ();
	}

	void Start(){
		StartDestroyCountDown ();
	}

	void StartDestroyCountDown(){

	}

	protected virtual void Update(){
		timeSpendInSeconds += Time.deltaTime * allAroundSpeed.allAroundSpeed;

		if(timeSpendInSeconds > lifeTimeInSeconds){
			DestroyTrap();
		}
	}

	void DestroyTrap(){
		currentTile.GetComponent<Tile> ().EmptyTile ();
		Destroy (gameObject);
	}

	public void SetCurrentTile(GameObject tile){
		currentTile = tile;
	}
}
