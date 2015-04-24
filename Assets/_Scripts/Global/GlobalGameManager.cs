using UnityEngine;
using System.Collections;

public class GlobalGameManager : MonoBehaviour {
	
	// Use this for initialization
	void Awake () {
		gameObject.AddComponent<GameSpeedFocus> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
