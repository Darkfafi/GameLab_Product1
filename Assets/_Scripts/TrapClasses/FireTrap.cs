using UnityEngine;
using System.Collections;

public class FireTrap : TouchTrap {

	bool canStrike = false;
	public bool sortLayer = true;

	void Start(){
		if(!sortLayer){
			if(GetComponent<SortLayer>() != null){
				Destroy(GetComponent<SortLayer>());
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.GetComponent<Lives>() != null && canStrike){
			animator.Play("End");
		}
	}
	protected override void TriggerSpike ()
	{
		if(canStrike){
			base.TriggerSpike ();
		}
	}

	protected override void Update ()
	{
		base.Update ();
		if(gameManEffect.timeSpendInSeconds  > 5.3f && gameManEffect.timeSpendInSeconds  < 6f){
			EndOpening();
		}
	}
	void ActivatedTrap(){

		canDamage = true;
		canStrike = true;

		TriggerSpike ();
	}

	void EndOpening(){
		animator.Play("End");
	}

	void Invisable(){
		Color color;
		color = GetComponent<SpriteRenderer> ().color;
		color.a = 0f;
		GetComponent<SpriteRenderer> ().color = color;
	}

	void Visable(){
		Color color;
		color = GetComponent<SpriteRenderer> ().color;
		color.a = 1f;
		GetComponent<SpriteRenderer> ().color = color;
	}
}
