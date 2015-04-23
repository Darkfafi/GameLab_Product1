using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float speed = 5;

	// Update is called once per frame
	void Update () {
		//Debug.Log (Time.deltaTime);
		Vector3 velocityControll = new Vector3 (Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical"),0);
		rigidbody2D.velocity = velocityControll * speed;
		//transform.Translate (new Vector2 (Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical")) * Time.deltaTime * speed);;
	}
}
