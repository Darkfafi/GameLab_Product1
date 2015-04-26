using UnityEngine;
using System.Collections;

public class GameSpeedManipulationEffect : MonoBehaviour {

	GameSpeedFocus gameSpeedFocus;
	AllAroundSpeed gameObjectSpeed;

	private float _timeSpendInSeconds = 0f;

	public bool manipulateAnimationSystem = true; 

	void Awake(){
		if(gameObject.GetComponent<AllAroundSpeed>() == null){
			gameObject.AddComponent<AllAroundSpeed>();
		}
	}

	void Start(){
		gameSpeedFocus = GameObject.Find ("GameController").GetComponent<GameSpeedFocus> ();
		gameObjectSpeed = GetComponent<AllAroundSpeed>();
		gameObjectSpeed.allAroundSpeed = gameSpeedFocus.gameSpeed;
	}

	void Update(){
		gameObjectSpeed.allAroundSpeed = gameSpeedFocus.gameSpeed;

		_timeSpendInSeconds += Time.deltaTime * gameSpeedFocus.gameSpeed;

		if(manipulateAnimationSystem){
			if (gameObject.GetComponent<Animator> () != null) {
				gameObject.GetComponent<Animator> ().speed = 1 * gameObjectSpeed.allAroundSpeed;
			}
		}
	}

	public float timeSpendInSeconds{
		get{return _timeSpendInSeconds;}
	}
}
