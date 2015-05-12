 using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour {
	
	public GameObject endScreen;

	void Start(){
		GameObject.Find ("Player").GetComponent<Lives> ().LostLifeEvent += PlayerLostLife;
	}
	
	// Update is called once per frame
	void PlayerLostLife (int lives) {

		if(lives >= 0){
			GetComponent<Animator> ().Play ("lives" + lives.ToString());
			//
		}
	}

	void ShowEndScreen(){
		endScreen.SetActive(true);
	}
}
