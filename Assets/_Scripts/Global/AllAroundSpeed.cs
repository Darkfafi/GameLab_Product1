﻿using UnityEngine;
using System.Collections;

public class AllAroundSpeed : MonoBehaviour {

	public float allAroundSpeed = 1;

	void Awake(){
		gameObject.AddComponent<SortLayer> ();
	}
}
