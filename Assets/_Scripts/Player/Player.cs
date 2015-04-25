﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	// Use this for initialization
	Animator animator;

	void Awake () {
		animator = GetComponent<Animator> ();
		gameObject.AddComponent<AllAroundSpeed> ();
		gameObject.AddComponent<PlayerMovement> ();
	}
}
