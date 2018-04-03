using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleSlider : MonoBehaviour {

	public GameObject canvas;
	public Text sliderText; 

	public void toggleAndIncrement(){
		canvas.SetActive(true);
		sliderText.text = int.Parse(sliderText.text + 1).ToString();

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.Get(OVRInput.Button.One)){
			toggleAndIncrement();
		}
	}
}
