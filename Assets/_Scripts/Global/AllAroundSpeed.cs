using UnityEngine;
using System.Collections;

public class AllAroundSpeed : MonoBehaviour {

	public float allAroundSpeed = 1;


	void Update(){
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}
}
