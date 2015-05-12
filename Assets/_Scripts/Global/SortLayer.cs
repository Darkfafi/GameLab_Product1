using UnityEngine;
using System.Collections;

public class SortLayer : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<SpriteRenderer>() != null){
			GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
		}
	}
}
