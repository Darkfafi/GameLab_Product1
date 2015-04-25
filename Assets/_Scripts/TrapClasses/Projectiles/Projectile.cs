using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private int hitCount = 0;

	private float _speed;
	private AllAroundSpeed allAroundSpeed;
	private Vector2 direction;

	// Use this for initialization
	void Awake () {
		allAroundSpeed = gameObject.AddComponent<AllAroundSpeed> ();
		gameObject.AddComponent<GameSpeedManipulationEffect> ();
		gameObject.AddComponent<BoxCollider2D> ();
		gameObject.AddComponent<Rigidbody2D> ();
		rigidbody2D.gravityScale = 0;
		collider2D.isTrigger = true;
	}

	public float speed{
		set{ _speed = value;}
	}

	// Update is called once per frame
	void Update () {

		direction = new Vector2 (Mathf.Cos((transform.eulerAngles.z - 90) / 180 * Mathf.PI),Mathf.Sin((transform.eulerAngles.z - 90) / 180 * Mathf.PI));
		rigidbody2D.velocity = (direction * _speed * allAroundSpeed.allAroundSpeed);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag != "GroundTile"){
			if(hitCount != 0){
				if(other.gameObject.GetComponent<Lives>() != null){
					other.gameObject.GetComponent<Lives>().AddSubLife(-1);
				}
				Destroy (gameObject);
			}
			hitCount += 1;
		}
	}
}
