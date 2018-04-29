using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

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
			for(int i = 0; i < copyOfScenesArray.Length; i++){
				Debug.Log(copyOfScenesArray[i]);
			}
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
		//log to file needs to be here
		prompted = true;
		AnxietyReminder.SetActive(true);
		
	}

	public void changeFlags(){
		ratedInitialAnxiety = true;
		instruction.text = "Press either <b>Trigger</b> to begin.";
		//log to file needs to be here
	}

	public void LogAndTransition(){
		//log to file needs to be here
		if(sceneCount < 0 || sceneCount >= copyOfScenesArray.Length)
		{
			SceneManager.LoadScene("Goodbye");
		}
		else
		{
		SceneManager.LoadScene(copyOfScenesArray[sceneCount]);
		}
	}
	// Update is called once per frame
	void Update () {
		//if anxiety slider is manipulated, wait five seconds then call changeFlags(), which changes instruction text and marks initialanxiety as true.
		
		if(scene.name != "Tutorial"){
			if((OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKey("2") || Input.GetKey("1")) && !invoked){
				Debug.Log("rated initial");
				Invoke("changeFlags", 5);
				invoked = true;
			}
			//then, if ratedInitialAnxiety & the trigger buttons are pressed, mark pressedTrigger as true
			if((OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKey("9")) && ratedInitialAnxiety){
				pressedTrigger = true;
				Debug.Log("pressed trigger");
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
					Debug.Log("canvas closed, invoked RateAtTenSeconds");
					Invoke("RateAtTenSeconds",10);
					invokedb = true;
				}
				if(invokedb && prompted){
					Debug.Log("prompted to rate anxiety during RateAtTenSeconds");
					if(OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKey("2") || Input.GetKey("1")){
						AnxietyReminder.SetActive(false);
						//invoke some kind of timer for 20 seconds to log first, then go to next scene
						Invoke("LogAndTransition",20);
						Debug.Log("invoked LogAndTransition");
					}
				}
				return;
			}
		}
		else
		{
			if(GlobalVariables.tutorialDone){
				SceneManager.LoadScene(copyOfScenesArray[sceneCount]);
			}
		}
	}
}
