using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	bool _timerRunning = true;
	float _currentTimeInDeltaTime = 0;

	// Update is called once per frame
	void Update () {
		if(_timerRunning){
			_currentTimeInDeltaTime += Time.deltaTime;
		}
	}

	float GetCurrentTimeInSeconds(){
		return _currentTimeInDeltaTime;
	}

	string MorphTimeToHumanTimeString(float timeInSeconds){

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

		return hoursCounter.ToString () + ":" + minutesCounter.ToString () + ":" + secondsCounter.ToString () + ":" + milisecondsCounter.ToString ();
	}

	void ToggleTimer(bool toggleSet){
		_timerRunning = toggleSet;
	}

	void ResetTimer(){
		_currentTimeInDeltaTime = 0;
	}
}
