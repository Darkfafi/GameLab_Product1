﻿using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Vector2 tileGridPosition = new Vector2();
	public int typeId = 0; //of het op de muur zit (1) of op de grond (0) etc
	public int containId = 0; //of er niks op zit (0) of een trap (1) etc
	public bool pressured = false; //kan je zien of er iets op deze tile staat (bijv speler staat er op dan spawn een trap daar).


	public Vector2 tileSize = new Vector2 (0.8484f, 0.83426f);

	void Awake(){
		if(GetComponent<BoxCollider2D>() != null){
			Vector3 size = new Vector3(tileSize.x,tileSize.y,1);
			GetComponent<BoxCollider2D>().size = size;
		}
	}

	public void AddTrap(GameObject trap,Quaternion rotation){
		containId = 1;
		trap = Instantiate (trap, transform.position, rotation) as GameObject; //als tile grid position aan begin van x is dan rotate hem naar rechts
		trap.GetComponent<Trap> ().SetCurrentTile(gameObject);
	}

	public void EmptyTile(){
		containId = 0;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player" && gameObject.tag != "Wall"){
			pressured = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player" && gameObject.tag != "Wall"){
			pressured = false;//
		}
	}
}