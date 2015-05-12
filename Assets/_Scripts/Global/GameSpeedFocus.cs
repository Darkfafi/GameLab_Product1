using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSpeedFocus : MonoBehaviour {

	//moet gelinkt worden met de neurosky.

	private float _gameSpeed = 1f;

	private bool slidingSlider = false;
	private float sliderValue = 0;
	private float sliderSlideSpeed = 0.002f;
	GameObject slider;
	public float gameSpeed{
		
		get{ return _gameSpeed; }
	}

	void Start(){
		if(GameObject.Find("NeuroSkyTGCController") != null){
			TGCConnectionController controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
			slider = GameObject.Find ("Bar");
			controller.UpdateAttentionEvent += OnUpdateAttention;
		}
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
			if(Mathf.Abs(slider.transform.localScale.y - sliderValue) > 0.01f){
				if(slider.transform.localScale.y < sliderValue){
					slider.transform.localScale = new Vector3 (1, slider.transform.localScale.y + sliderSlideSpeed, 1);
				}else if (slider.transform.localScale.y > sliderValue){
					slider.transform.localScale = new Vector3 (1, slider.transform.localScale.y - sliderSlideSpeed, 1);
					//.gameObject.transform.localScale = new Vector3 (1, 0.5f, 1)
				}

				if(slider.transform.localScale.y > 0.65f){
					if(GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().mute){

						GetComponent<AudioSource>().volume = 1.05f - slider.transform.localScale.y;
						GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().volume = slider.transform.localScale.y - 0.25f;

						GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().mute = false;
					}else{
						GetComponent<AudioSource>().volume = 1.05f - slider.transform.localScale.y;
						GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().volume = slider.transform.localScale.y - 0.20f;
					}
				}else{
					GetComponent<AudioSource>().volume = 0.8f;
					GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().mute = true;
				}
			}else{
				slidingSlider = false;
			}
			_gameSpeed = 1.3f - slider.transform.localScale.y;	
		}
	}
}
