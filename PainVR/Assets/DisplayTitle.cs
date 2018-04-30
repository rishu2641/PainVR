using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;


/* Although this class is vaguely called DisplayTitle, it really handles the timing logic for progressing through scenes in a new user's first go at OPAL */

public class DisplayTitle : MonoBehaviour {

	public GameObject SceneTitleCanvas;
	public GameObject AnxietyReminder;
	public GameObject player;
	public Text title;
	public Text instruction;
	public Scene scene;
	public int sceneCount;
	bool pressedTrigger = false;
	bool ratedInitialAnxiety = false;
	bool invoked = false;
	bool invokedb = false;
	bool prompted = false;
	static public bool isRandomized = false;
	Vector3 pos;
	static public string[] copyOfScenesArray = new string[GlobalVariables.Scenes.Length];

	public void closeCanvas(){
		SceneTitleCanvas.SetActive(false);
	}

	public string[] randomizeScenes(){
		System.Random rnd=new System.Random();
		return GlobalVariables.Scenes.OrderBy(x => rnd.Next()).ToArray();
	}
	// Use this for initialization
	void Start () {
		//fetch the current scene
		scene = SceneManager.GetActiveScene();
		if(!isRandomized){
			copyOfScenesArray = randomizeScenes();
			isRandomized = true;
		}
		sceneCount = Array.IndexOf(copyOfScenesArray, scene.name) + 1;
		//fetch the instruction text from GlobalVariables.cs
		title.text = GlobalVariables.SceneInstructions[scene.name];
		//set instruction text
		instruction.text = "Please <b>rate your initial anxiety</b> level now.";
		//fetch the player's position
		pos = player.transform.position;
	}
	
	public void RateAtTenSeconds(){
		//log anxiety value when prompt to rate anxiety appears Math.Floor(Time.time - GlobalVariables.startTime)
		GlobalFunction.LogToPatientFile(GlobalVariables.Filename, scene.name, "At 10 Seconds", GlobalVariables.sliderValue);
		prompted = true;
		AnxietyReminder.SetActive(true);
		
	}

	public void changeFlags(){
		ratedInitialAnxiety = true;
		instruction.text = "Press either <b>Trigger</b> to begin.";
		GlobalFunction.LogToPatientFile(GlobalVariables.Filename, scene.name, "Initial", GlobalVariables.sliderValue);
	}

	public void LogAndTransition(){
		//log to file
		GlobalFunction.LogToPatientFile(GlobalVariables.Filename, scene.name, "Final", GlobalVariables.sliderValue);

		if(sceneCount < 0 || sceneCount >= copyOfScenesArray.Length)
		{
			SceneManager.LoadScene("Goodbye");
		}
		else
		{
		SceneManager.LoadScene(copyOfScenesArray[sceneCount]);
		}
	}

	void Update () {		
		if(scene.name != "Tutorial"){
			if((OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKey("2") || Input.GetKey("1")) && !invoked){
				Invoke("changeFlags", 5);
				invoked = true;
			}
			//then, if ratedInitialAnxiety & the trigger buttons are pressed, mark pressedTrigger as true
			if((OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKey("9")) && ratedInitialAnxiety){
				pressedTrigger = true;
			}
			
			//if !pressedtrigger and !ratedinitialanxiety, keep the player at the current position
			if(!(pressedTrigger && ratedInitialAnxiety))
			{
				player.transform.position = pos;
			}
			//else, closecanvas.
			else{
				closeCanvas();
				if(!invokedb){
					Invoke("RateAtTenSeconds",10);
					invokedb = true;
				}
				if(invokedb && prompted){
					if(OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKey("2") || Input.GetKey("1")){
						AnxietyReminder.SetActive(false);
						Invoke("LogAndTransition",20);
					}
				}
				return;
			}
		}
		else
		{
			if(GlobalVariables.tutorialDone){
				GlobalVariables.startTime = Math.Floor(Time.time);
				SceneManager.LoadScene(copyOfScenesArray[sceneCount]);
			}
		}
	}
}
