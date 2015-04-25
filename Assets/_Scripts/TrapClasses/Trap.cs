using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Trap : MonoBehaviour {

	public float lifeTimeInSeconds = 5f; // 0 betekend dat het vernietigd word wanneer het zelf erom vraagt

	protected float timeSpendInSeconds = 0f;

	protected AllAroundSpeed allAroundSpeed;

	protected Animator animator;

	private GameObject currentTile;

	void Awake(){
		if(GetComponent<Animator>() != null){
			animator = GetComponent<Animator>();
		}

		allAroundSpeed = gameObject.AddComponent<AllAroundSpeed> ();
		gameObject.AddComponent<GameSpeedManipulationEffect> ();
	}

	protected virtual void Update(){
		timeSpendInSeconds += Time.deltaTime * allAroundSpeed.allAroundSpeed;

		if(lifeTimeInSeconds != 0 && timeSpendInSeconds > lifeTimeInSeconds){
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
