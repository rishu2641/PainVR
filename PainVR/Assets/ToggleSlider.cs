using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ToggleSlider : MonoBehaviour {

	public GameObject tutorialCanvas1;
	public GameObject tutorialCanvas2;
	public GameObject tutorialCanvas3;
	double belowThresholdTimeTutorial;
	bool belowThresholdTutorial	= true;
	bool stepOne = true;
	bool stepTwo = false;
	bool TutorialDone = false;
	public Rigidbody tutorialSphere;

	public int threshold = 20;
	public GameObject canvas;
	public Text sliderText;
	public Slider slider;
	public static string[] scenes = {"Tutorial", "apple-1-supermarket", "apple-2-supermarket", "apple-3-cafeteria", "apple-4-cafeteria", "apple-5-cafeteria","apple-6-cafeteria"};
	double startingtime;
	double heldTimeA;
	double heldTimeB;

	//scene transition variables
	double belowThresholdTime;
	bool belowThreshold = false;
	static public int sceneCount = 0;

	bool isActive;
	public int maxValue = 100;
	public int minValue = 0;
	int val = 50;

	//logging to csv variables
	string fileText = System.IO.File.ReadAllText(GlobalVariables.Filename);
	List<int> tempStats = new List<int>();

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

	public void LogToFile(){
		tempStats.Add(Convert.ToInt32(Math.Round(slider.value)));
		fileText += "\n" + Math.Floor(Time.time) + "," + scenes[sceneCount] + "," + slider.value + ",,";
	}

	public string AverageAnxietyLevels(){
		double sum = 0;
		for (int i = 0; i < tempStats.Count; i++){
			sum += tempStats[i];
		}
		return Math.Floor(sum/tempStats.Count).ToString();
	}
	// Use this for initialization
	void Start () {
		tempStats.Clear();
		InvokeRepeating("LogToFile", 1.0f, 2.0f);
		slider.value = 50;
		belowThresholdTutorial	= true;
		stepOne = true;
		stepTwo = false;
		TutorialDone = false;
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

		if(sceneCount == 0 && stepOne){
			if(val < 100){
				belowThresholdTutorial = true;
			}
			if(val >= 100 && belowThresholdTutorial){
				belowThresholdTimeTutorial = ConvertToUnixTimestamp(DateTime.Now);
				belowThresholdTutorial	= false;
			}
			if(ConvertToUnixTimestamp(DateTime.Now) - belowThresholdTimeTutorial >= 5 && !belowThresholdTutorial){
				tutorialCanvas1.SetActive(false);
				tutorialCanvas2.SetActive(true);
				tutorialCanvas3.SetActive(false);
				stepTwo	= true;
				stepOne = false;
				TutorialDone = false;
			}
		}
		else if(stepTwo){
			if(tutorialSphere.isKinematic){
				tutorialCanvas1.SetActive(false);
				tutorialCanvas2.SetActive(false);
				tutorialCanvas3.SetActive(true);
				stepTwo	= false;
				stepOne = false;
				TutorialDone = true;
			}
		}
		else {
			TutorialDone = true;
		}
		if(TutorialDone){
			//scene transitioning
			if(val >= threshold){
				belowThreshold = false;
			}
			if(val < threshold && !belowThreshold){
					belowThresholdTime = ConvertToUnixTimestamp(DateTime.Now);
					belowThreshold = true;
			}
			if(ConvertToUnixTimestamp(DateTime.Now) - belowThresholdTime >= 5 && belowThreshold){
				string average = AverageAnxietyLevels();
				fileText = fileText.Substring(0, fileText.Length-1) + scenes[sceneCount] + "," + average;
				System.IO.File.WriteAllText(GlobalVariables.Filename, fileText);
				sceneCount++;
				if(sceneCount >= scenes.Length || sceneCount < 0){
					sceneCount =0;
				}
				SceneManager.LoadScene(scenes[sceneCount]);
			}
		}
	}

}
