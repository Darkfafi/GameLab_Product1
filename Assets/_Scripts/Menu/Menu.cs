using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	float signalValue = 200;

	void Start(){
		TGCConnectionController controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();

		controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;
	}

	void OnUpdatePoorSignal(int value){
		signalValue = value;
	}

	// Update is called once per frame
	void Update () {
		if(signalValue < 70){
			if(Input.anyKeyDown){
				if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton1)){
					Debug.Log("Quit");
					Application.Quit();
				}else{
					Debug.Log("Start");
					Application.LoadLevel("Test");
				}
			}
		}
	}
}
