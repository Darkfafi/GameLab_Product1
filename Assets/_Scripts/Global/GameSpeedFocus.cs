using UnityEngine;
using System.Collections;

public class GameSpeedFocus : MonoBehaviour {

	//moet gelinkt worden met de neurosky.

	private float _gameSpeed = 0.7f;

	public float gameSpeed{
		
		get{ return _gameSpeed; }
	}
}
