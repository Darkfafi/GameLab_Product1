using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSpeedFocus : MonoBehaviour {

	//moet gelinkt worden met de neurosky.

	private float _gameSpeed = 1f;

	private bool slidingSlider = false;
	private float sliderValue = 0;
	private float sliderSlideSpeed = 0.002f;
	Slider slider;
	public float gameSpeed{
		
		get{ return _gameSpeed; }
	}

	void Start(){
		TGCConnectionController controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
		slider = GameObject.Find ("FocusBar").GetComponent<Slider> ();
		controller.UpdateAttentionEvent += OnUpdateAttention;
	}

	void OnUpdateAttention(int value){
		float speedValue = value * 0.01f;
		SlideSliderToValue(speedValue);
	}

	void SlideSliderToValue(float value){
		slidingSlider = true;
		sliderValue = value;
	}

	void Update(){
		if(slidingSlider){
			Debug.Log(Mathf.Abs(slider.value - sliderValue));
			if(Mathf.Abs(slider.value - sliderValue) > 0.01f){
				if(slider.value < sliderValue){
					slider.value += sliderSlideSpeed;
				}else if (slider.value > sliderValue){
					slider.value -= sliderSlideSpeed;

				}

				if(slider.value > 0.75f){
					if(GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().mute){
						GetComponent<AudioSource>().volume = 1.15f - slider.value;
						GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().mute = false;
						GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().volume = slider.value - 0.25f;
					}
				}else{
					GetComponent<AudioSource>().volume = 0.8f;
					GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().mute = true;
				}
			}else{
				slidingSlider = false;
			}
			_gameSpeed = 1.3f - (slider.value);
		}
	}
}
