using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Color originalColor;

	// Use this for initialization
	Animator animator;

	void Awake () {
		animator = GetComponent<Animator> ();
		gameObject.AddComponent<AllAroundSpeed> ();
		gameObject.AddComponent<PlayerMovement> ();

		originalColor = GetComponent<SpriteRenderer>().color;
	}

	void NoLivesLeft(){
		//play death animation. After death animation show end screen.
		Destroy (GetComponent<PlayerMovement>());
		Destroy (GetComponent<Rigidbody2D> ());
		animator.Play("Death");
	}

	void LifeCooldownStarted(){
		if(GetComponent<Lives>().lives != 0){
			GetComponent<SpriteRenderer>().color = new Color(120f,0f,0f,0.6f);
		}
	}

	void LifeCooldownEnded(){
		GetComponent<SpriteRenderer> ().color = originalColor;
	}
}
