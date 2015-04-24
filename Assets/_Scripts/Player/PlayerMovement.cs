using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float speed = 5;
	private AllAroundSpeed allAroundSpeed;

	void Awake(){
		allAroundSpeed = GetComponent<AllAroundSpeed> ();
	}

	void Update () {
		Vector3 velocityControll = new Vector3 (Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical"),0);
		rigidbody2D.velocity = velocityControll * speed * allAroundSpeed.allAroundSpeed;
	}
}
