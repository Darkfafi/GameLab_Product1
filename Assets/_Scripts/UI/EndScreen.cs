using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 0.3f;
		GameObject.Find ("Timer").GetComponentInChildren<Timer> ().ToggleTimer (false);
		GameObject.Find ("TimeText").GetComponent<Text> ().text = GameObject.Find ("Timer").GetComponentInChildren<Timer> ().MorphTimeToHumanTimeString (GameObject.Find ("Timer").GetComponentInChildren<Timer> ().GetCurrentTimeInSeconds ());
		GameObject.Find ("GameController").GetComponent<AudioSource> ().volume = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.JoystickButton2)){
			Application.LoadLevel("Test");
			Time.timeScale = 1f;
		}
		if(Input.GetKeyDown(KeyCode.JoystickButton0)){
			Application.LoadLevel("Menu");
			Time.timeScale = 1f;
		}
		if(Input.GetKeyDown(KeyCode.JoystickButton1)){
			Application.Quit();
			Time.timeScale = 1f;
		}
	}
}
