using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Vector2 tileGridPosition = new Vector2();
	public int typeId = 0; //of het op de muur zit (1) of op de grond (0) etc
	public int containId = 0; //of er niks op zit (0) of een trap (1) etc
	public bool pressured = false; //kan je zien of er iets op deze tile staat (bijv speler staat er op dan spawn een trap daar).
	private float timePressured = 0;
	private bool _pressureTrapActive = false;

	public Vector2 tileSize = new Vector2 (0.8484f, 0.83426f);

	void Awake(){
		if(GetComponent<BoxCollider2D>() != null){
			Vector3 size = new Vector3(tileSize.x,tileSize.y,1);
			GetComponent<BoxCollider2D>().size = size;
		}
		Invoke ("ActivatePlateTrap", 18f);
	}
	void ActivatePlateTrap(){
		_pressureTrapActive = true;
	}
	public void AddTrap(GameObject trap,Quaternion rotation){
		trap = Instantiate (trap, transform.position, rotation) as GameObject; //als tile grid position aan begin van x is dan rotate hem naar rechts
		if(trap.GetComponent<Trap>().needsEmptyTile){
			containId = 1;
			trap.GetComponent<Trap> ().SetCurrentTile(gameObject);
		}
	}

	public void EmptyTile(){
		containId = 0;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player" && gameObject.tag != "Wall"){
			pressured = true;
			timePressured = Time.time;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player" && gameObject.tag != "Wall"){
			pressured = false;//
			timePressured = 0;
		}
	}
	void Update(){
		if(timePressured != 0 && _pressureTrapActive){
			if(containId == 0 && Time.time > timePressured + 5f){

				int trapToSpawn = Random.Range(0,2);
				string trapResourceString = "";
				if(trapToSpawn == 0){
					trapResourceString = "Prefabs/Traps/GroundTraps/SpikeTrap";
				}else if (trapToSpawn == 1){
					trapResourceString = "Prefabs/Traps/GroundTraps/LightningStrikeTrap";
				}
				GameObject trap = Resources.Load(trapResourceString) as GameObject;
				AddTrap(trap,transform.rotation);
				timePressured = Time.time;
			}
		}
	}
}