using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {
	
	public int lives = 3;

	private bool adjustAble = true;

	public float secondsCooldownAfterHit = 0.5f;

	private float secondsPassed = 0f;

	public void AddSubLife(int amount){
		if(adjustAble){
			lives += 1;
			if(amount < 0){
				SendMessage("LostLife",SendMessageOptions.DontRequireReceiver);
				if(lives < 0){
					SendMessage("NoLivesLeft",SendMessageOptions.DontRequireReceiver);
				}
				HitLessCountdownStart();
			}else if(amount > 0){
				SendMessage("AddedLife",SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void HitLessCountdownStart(){
		adjustAble = false;
		SendMessage ("LifeCooldownStarted", SendMessageOptions.DontRequireReceiver);
	}

	void Update(){
		if(!adjustAble){
			secondsPassed += Time.deltaTime;
			if(secondsPassed > secondsCooldownAfterHit){
				adjustAble = true;
				secondsPassed = 0f;
				SendMessage("LifeCooldownEnded",SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
