using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FadeInOut : MonoBehaviour {

	private Color _originalColor;

	private bool _fading = false;

	private float _fadeSpeed = 0.005f;
	private float _fadeTargetValue = 0;

	private float _timeAskedForTimerFadeOut = 0;
	private float _timerFadeOut = float.NaN;
	private float _fadeSpeedTimer = 0.005f;
	private float _fadeTargetValueTimer = 0;


	public delegate void FloatInfo(float value);

	public event FloatInfo OnFadeStart;
	public event FloatInfo OnFade;
	public event FloatInfo OnFadeEnd;

	void Start(){
		_originalColor = GetColor ();
	}

	Color GetColor(){
		Color returnColor = new Color();
		if(GetComponent<SpriteRenderer>() != null){
			returnColor = GetComponent<SpriteRenderer>().color;
		}else if(GetComponent<CanvasRenderer>() != null){
			returnColor = GetComponent<CanvasRenderer>().GetColor();
		}
		return returnColor;
	}

	void SetColor(Color color){
		if(GetComponent<SpriteRenderer>() != null){
			GetComponent<SpriteRenderer>().color = color;
			for(int i = 0; i < gameObject.transform.childCount; i++)
			{
				GameObject Go = gameObject.transform.GetChild(i).gameObject;
				if(Go.GetComponent<SpriteRenderer>() != null){
					Go.GetComponent<SpriteRenderer>().color = color;
				}
			}
		}else if(GetComponent<CanvasRenderer>() != null){
			GetComponent<CanvasRenderer>().SetColor(color);

			for(int i = 0; i < gameObject.transform.childCount; i++)
			{
				GameObject Go = gameObject.transform.GetChild(i).gameObject;
				if(Go.GetComponent<CanvasRenderer>() != null){
					Go.GetComponent<CanvasRenderer>().SetColor(color);
				}
			}
		}
	}

	public void Fade(float fadeToValue,float fadeSpeed = 0.005f){
		_fadeTargetValue = fadeToValue;
		_fadeSpeed = fadeSpeed;
		_fading = true;
		if(OnFadeStart != null){
			OnFadeStart(GetColor().a);
		}
	}
	public void FadeAfterTime(float timeInSeconds,float fadeToValue,float fadeSpeed = 0.005f){
		_timeAskedForTimerFadeOut = Time.time;
		_fadeTargetValueTimer = fadeToValue;
		_fadeSpeedTimer = fadeSpeed;
		_timerFadeOut = timeInSeconds;
	}

	// Update is called once per frame
	void Update () {

		if (!float.IsNaN(_timerFadeOut)) {
			if(Time.time > _timeAskedForTimerFadeOut + _timerFadeOut){
				_timerFadeOut = float.NaN;
				Fade(_fadeTargetValueTimer,_fadeSpeedTimer);
			}
		}

		if(_fading){
			int dir = 0;
			Color color = _originalColor;

			if(_fadeTargetValue < color.a){
				dir = -1;
			}else if(_fadeTargetValue > color.a){
				dir = 1;
			}
			color.a += dir * _fadeSpeed;
			SetColor(color);
			_originalColor = GetColor();

			if(OnFade != null){
				OnFade(GetColor().a);
			}

			if(color.a >= _fadeTargetValue && dir == 1 || color.a <= _fadeTargetValue && dir == -1){
				_fading = false;
				if(OnFadeEnd != null){
					OnFadeEnd(GetColor().a);
				}
			}
		}
	}
}
