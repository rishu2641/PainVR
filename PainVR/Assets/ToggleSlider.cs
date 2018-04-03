using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ToggleSlider : MonoBehaviour {

	public int threshold = 20;
	public GameObject canvas;
	public Text sliderText;
	public Slider slider;
	public string[] scenes = {"apple-1-supermarket", "apple-2-supermarket", "apple-3-cafeteria", "apple-4-cafeteria", "apple-5-cafeteria","apple-6-cafeteria"};
	double startingtime;
	double heldTimeA;
	double heldTimeB;
	
	//scene transition variables
	double belowThresholdTime;
	bool belowThreshold = false;
	static public int sceneCount;

	bool isActive;
	public int maxValue = 100;
	public int minValue = 0;
	int val = 50;

	public static double ConvertToUnixTimestamp(DateTime date){
	    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
	    TimeSpan diff = date.ToUniversalTime() - origin;
	    return Math.Floor(diff.TotalSeconds);
	}

	public void toggleAndIncrement(){
		startingtime = ConvertToUnixTimestamp(DateTime.Now);
		canvas.SetActive(true);
		if(int.Parse(sliderText.text) < maxValue){
			val = int.Parse(sliderText.text) + 1;
		}
		else{
			val = maxValue;
		}
		sliderText.text = val.ToString();
		slider.value = val;
		isActive = true;
	}
	public void toggleAndDecrement(){
		startingtime = ConvertToUnixTimestamp(DateTime.Now);
		canvas.SetActive(true);
		if(int.Parse(sliderText.text) > minValue){
			val = int.Parse(sliderText.text) - 1;
		}
		else{
			val = minValue;
		}
		sliderText.text = val.ToString();
		slider.value = val;
		isActive = true;
	}
	// Use this for initialization
	void Start () {
		slider.value = 50;
	}
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.GetDown(OVRInput.Button.One)){
			heldTimeA = ConvertToUnixTimestamp(DateTime.Now);
			toggleAndIncrement();
		}
		else if(OVRInput.Get(OVRInput.Button.One)){
			if(ConvertToUnixTimestamp(DateTime.Now) - heldTimeA >= .75){
				toggleAndIncrement();
			}
		}
		if(OVRInput.GetDown(OVRInput.Button.Two)){
			heldTimeB = ConvertToUnixTimestamp(DateTime.Now);
			toggleAndDecrement();
		}
		else if(OVRInput.Get(OVRInput.Button.Two)){
			if(ConvertToUnixTimestamp(DateTime.Now) - heldTimeB >= .75){
				toggleAndDecrement();
			}
		}
		if(isActive && ConvertToUnixTimestamp(DateTime.Now) - startingtime >= 5){
        	canvas.SetActive(false);
        	isActive = false;
		}

		//scene transitioning
		if(val >= 20){
			belowThreshold = false;
		}
		if(val < 20 && !belowThreshold){
				belowThresholdTime = ConvertToUnixTimestamp(DateTime.Now);
				belowThreshold = true;
		}
		if(ConvertToUnixTimestamp(DateTime.Now) - belowThresholdTime >= 5 && belowThreshold){
			sceneCount++;
			if(sceneCount >= scenes.Length || sceneCount < 0){
				sceneCount =0;
			}
			SceneManager.LoadScene(scenes[sceneCount]);
		}
	}

}
