using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Vector2 tileGridPosition = new Vector2();
	public int typeId = 0; //of het op de muur zit (1) of op de grond (0) etc
	public int containId = 0; //of er niks op zit (0) of een trap (1) etc


	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			Debug.Log(tileGridPosition);
		}
	}

	public void AddTrap(GameObject trap,Quaternion rotation){
		containId = 1;
		Debug.Log (rotation);
		trap = Instantiate (trap, transform.position, rotation) as GameObject; //als tile grid position aan begin van x is dan rotate hem naar rechts
		trap.GetComponent<Trap> ().currentTile = gameObject;
	}

	public void EmptyTile(){
		containId = 0;
	}
}