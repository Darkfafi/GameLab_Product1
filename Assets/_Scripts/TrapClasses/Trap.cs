using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Trap : MonoBehaviour {

	public float lifeTimeInSeconds = 5f; // 0 betekend dat het vernietigd word wanneer het zelf erom vraagt

	public bool needsEmptyTile = true;
	public bool sortLayer = true;
	public int trapPositionType = 0;
	protected AllAroundSpeed allAroundSpeed;
	protected GameSpeedManipulationEffect gameManEffect;
	protected Animator animator;

	private GameObject currentTile;

	void Awake(){
		if(GetComponent<Animator>() != null){
			animator = GetComponent<Animator>();
		}

		allAroundSpeed = gameObject.AddComponent<AllAroundSpeed> ();
		gameManEffect = gameObject.AddComponent<GameSpeedManipulationEffect> ();
		if(GetComponent<SpriteRenderer>().sprite != null){
			Instantiate (Resources.Load ("Prefabs/Traps/SmokeTrapSpawn"), transform.position - Vector3.forward * 0.3f, transform.rotation);
		}
		if(!sortLayer){
			if(GetComponent<SortLayer>() != null){
				Destroy(GetComponent<SortLayer>());
			}
		}
	}
	protected virtual void Update(){

		if(lifeTimeInSeconds != 0 && gameManEffect.timeSpendInSeconds > lifeTimeInSeconds){
			DestroyTrap();
		}
	}

	void DestroyTrap(){
		if (currentTile != null) {
			currentTile.GetComponent<Tile> ().EmptyTile ();
		}
		Instantiate (Resources.Load ("Prefabs/Traps/SmokeTrapSpawn"), transform.position - Vector3.forward * 0.3f, transform.rotation);
		Destroy (gameObject);
	}

	public void SetCurrentTile(GameObject tile){
		currentTile = tile;
	}

	void ActivateThroughAnimation(){
		BroadcastMessage ("ActivatedTrap", SendMessageOptions.DontRequireReceiver);
	}
}
