using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSpeedFocus : MonoBehaviour {

	//moet gelinkt worden met de neurosky.

	private float _gameSpeed = 1f;

	private bool slidingSlider = false;
	private float sliderValue = 0;
	private float sliderSlideSpeed = 0.001f;

	GameObject focusBarDrip;

	GameObject slider;
	public float gameSpeed{
		
		get{ return _gameSpeed; }
	}

	void Start(){
		if(GameObject.Find("NeuroSkyTGCController") != null){
			TGCConnectionController controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
			controller.UpdateAttentionEvent += OnUpdateAttention;
		}
		slider = GameObject.Find ("Bar");
		focusBarDrip = GameObject.Find ("FocusBarDrip");
		focusBarDrip.SetActive (false);

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
		if(Input.GetKey(KeyCode.Space)){
			if(GameObject.Find("NeuroSkyTGCController") == null){
				OnUpdateAttention(100);
			}
		}
		if(slidingSlider){
			if(focusBarDrip.activeSelf == false){
				focusBarDrip.SetActive(true);
			}

			if(Mathf.Abs(slider.transform.localScale.y - sliderValue) > 0.01f){
				if(slider.transform.localScale.y < sliderValue){
					slider.transform.localScale = new Vector3 (1, slider.transform.localScale.y + sliderSlideSpeed, 1);
				}else if (slider.transform.localScale.y > sliderValue){
					slider.transform.localScale = new Vector3 (1, slider.transform.localScale.y - sliderSlideSpeed, 1);
				}

				if(slider.transform.localScale.y > 0.65f){
					if(GameObject.Find("EffectMusicPlayer").GetComponent<AudioSource>().mute){

						GetComponent<AudioSource>().volume = 1.05f - slider.transform.localScale.y ;
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
				focusBarDrip.SetActive(false);
				slidingSlider = false;
			}
			_gameSpeed = 1.4f - slider.transform.localScale.y * 1.2f;	
		}
	}
}
