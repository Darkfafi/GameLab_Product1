using UnityEngine;
using System.Collections;

public class GameSpeedManipulationEffect : MonoBehaviour {

	GameSpeedFocus gameSpeedFocus;
	AllAroundSpeed gameObjectSpeed;

	void Start(){
		gameSpeedFocus = GameObject.Find ("GameController").GetComponent<GameSpeedFocus> ();
		gameObjectSpeed = GetComponent<AllAroundSpeed>();
		gameObjectSpeed.allAroundSpeed = gameSpeedFocus.gameSpeed;
	}

	void Update(){
		gameObjectSpeed.allAroundSpeed = gameSpeedFocus.gameSpeed;
		if (gameObject.GetComponent<Animator> () != null) {
			gameObject.GetComponent<Animator> ().speed = 1 * gameObjectSpeed.allAroundSpeed;
		}
	}
}
