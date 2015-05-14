using UnityEngine;
using System.Collections;

public class FocusBarTopPosition : MonoBehaviour {

	public GameObject focusBarTopPos;

	// Update is called once per frame
	void Update () {
		transform.position = focusBarTopPos.transform.position + (new Vector3(0,0.18f,0));
	}
}
