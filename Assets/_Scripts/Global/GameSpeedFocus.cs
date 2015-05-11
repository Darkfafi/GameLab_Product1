using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSpeedFocus : MonoBehaviour {

	//moet gelinkt worden met de neurosky.

	private float _gameSpeed = 1f;

	private bool slidingSlider = false;
	private float sliderValue = 0;
	public float gameSpeed{
		
		get{ return _gameSpeed; }
	}

	void Start(){
		TGCConnectionController controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();

		controller.UpdateAttentionEvent += OnUpdateAttention;
	}

	void OnUpdateAttention(int value){
		float speedValue = value * 0.01f;
		SlideSliderToValue(speedValue);
		_gameSpeed = 1 - (speedValue);
	}

	void SlideSliderToValue(float value){
		slidingSlider = true;
		sliderValue = value;
	}

	void Update(){
		if(slidingSlider){
			Slider slider = GameObject.Find ("FocusBar").GetComponent<Slider> ();
			Debug.Log(Mathf.Abs(slider.value - sliderValue));
			if(Mathf.Abs(slider.value - sliderValue) > 0.01f){
				if(slider.value < sliderValue){
					slider.value += 0.005f;
				}else if (slider.value > sliderValue){
					slider.value -= 0.005f;

				}
			}else{
				slidingSlider = false;
			}
		}
	}
}
