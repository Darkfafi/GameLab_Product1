using UnityEngine;
using System.Collections;

public class TouchTrap : Trap {
	
	public bool triggerOnCreation = false;

	protected bool canDamage = false;

	// Use this for initialization
	void Start () {
		if(triggerOnCreation){
			TriggerSpike();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.GetComponent<Lives>() != null){
			TriggerSpike();
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(canDamage){
			if(other.gameObject.GetComponent<Lives>() != null){
				other.gameObject.GetComponent<Lives>().AddSubLife(-1);
			}
		}
	}
	protected virtual void TriggerSpike(){
		animator.Play("Strike");
	}

	void CanDamage(){ //word aangeroepen door de animation.
		canDamage = true;
	}
	void CantDamage(){
		canDamage = false;
	}
}
