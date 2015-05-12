using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Menu : MonoBehaviour {

	float signalValue = 200;

	string noNeuroText = "Please connect the Neurosky and wear the Neurosky correctly to continue.";
	string neuroConnected = "Press any key to continue. \r\n Press 'B' or 'Escape' to exit the game.";

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
			if(GameObject.Find("StartGameText").GetComponent<Text>().text != neuroConnected){
				GameObject.Find("StartGameText").GetComponent<Text>().text = neuroConnected;
			}
			if(Input.anyKeyDown){
				if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton1)){
					Debug.Log("Quit");
					Application.Quit();
				}else{
					Debug.Log("Start");
					Application.LoadLevel("Test");
				}
			}
		}else{
			if(GameObject.Find("StartGameText").GetComponent<Text>().text != noNeuroText){
				GameObject.Find("StartGameText").GetComponent<Text>().text = noNeuroText;
			}
		}
	}
}
