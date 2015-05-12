using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	bool _timerRunning = true;
	float _currentTimeInDeltaTime = 0;

	Text timerText;

	void Start(){
		timerText = gameObject.GetComponent<Text>();

	}

	// Update is called once per frame
	void Update () {
		if(_timerRunning){
			_currentTimeInDeltaTime += Time.deltaTime;
			if(timerText != null){
				timerText.text = MorphTimeToHumanTimeString(GetCurrentTimeInSeconds());
			}
		}
	}

	public float GetCurrentTimeInSeconds(){
		return _currentTimeInDeltaTime;
	}

	public string MorphTimeToHumanTimeString(float timeInSeconds){

		int hoursCounter = 0;
		int minutesCounter = 0;
		int secondsCounter = 0;
		int milisecondsCounter = 0;

		while(timeInSeconds > 3600){
			timeInSeconds -= 3600;
			hoursCounter += 1;
		}
		while (timeInSeconds > 60) {
			timeInSeconds -= 60;
			minutesCounter += 1;
		}
		secondsCounter = (int)timeInSeconds;
		timeInSeconds -= secondsCounter;
		milisecondsCounter = (int)(timeInSeconds* 100);

		string miliString = ":" + milisecondsCounter.ToString();
		string secString = ":" + secondsCounter.ToString();
		string minString = minutesCounter.ToString();
		string hourString = hoursCounter.ToString () + ":";


		if(milisecondsCounter < 10){
			miliString = ":0" + milisecondsCounter.ToString(); 
		}
		if(secondsCounter < 10){
			secString = ":0" + secondsCounter.ToString(); 
		}
		if(minutesCounter < 10){
			minString = "0" + minutesCounter.ToString(); 
		}
		if(hoursCounter < 10){
			hourString = "";
		}

		return hourString + minString + secString;
	}

	public void ToggleTimer(bool toggleSet){
		_timerRunning = toggleSet;
	}

	void ResetTimer(){
		_currentTimeInDeltaTime = 0;
	}
}
