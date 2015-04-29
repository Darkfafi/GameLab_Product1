using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float speed = 3f;
	private AllAroundSpeed allAroundSpeed;

	void Awake(){
		allAroundSpeed = GetComponent<AllAroundSpeed> ();
	}

	void Update () {

		Vector3 velocityControll = new Vector3 (Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical"),0);
		rigidbody2D.velocity = velocityControll * speed * allAroundSpeed.allAroundSpeed;

		if(Mathf.Abs(velocityControll.x) > Mathf.Abs(velocityControll.y)){

			//animatie zijkant.
			float lookDirection = 1;
			GetComponent<Animator>().Play("SideWalk");
			if(velocityControll.x < 0){
				//Left
				lookDirection = -Mathf.Abs(transform.localScale.x);
			}else{
				//Right
				lookDirection = Mathf.Abs(transform.localScale.x); 
			}

			transform.localScale = new Vector3(lookDirection,transform.localScale.y,1);

		}else if(Mathf.Abs(velocityControll.y) > Mathf.Abs(velocityControll.x)){
			if(velocityControll.y < 0){
				//Down
				GetComponent<Animator>().Play("FrontWalk");
			}else{
				//Up
			}
		}else if(Mathf.Abs (velocityControll.x) + Mathf.Abs(velocityControll.y) < 0.1f){
			GetComponent<Animator>().Play("Idle");
		}
	}
}
