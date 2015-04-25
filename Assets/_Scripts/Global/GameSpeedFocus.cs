using UnityEngine;
using System.Collections;

public class GameSpeedFocus : MonoBehaviour {

	//moet gelinkt worden met de neurosky.

	private float _gameSpeed = 1f;

	public float gameSpeed{
		
		get{ return _gameSpeed; }
	}
}
