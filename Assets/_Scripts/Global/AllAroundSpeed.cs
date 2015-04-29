using UnityEngine;
using System.Collections;

public class AllAroundSpeed : MonoBehaviour {

	public float allAroundSpeed = 1;

	//deze class moet eigenlijk globaal zijn en niet op elk object.
	void Update(){
		if(GetComponent<SpriteRenderer>() != null){
			GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
		}
	}
}
