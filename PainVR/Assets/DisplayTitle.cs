using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayTitle : MonoBehaviour {

	public GameObject SceneTitleCanvas;
	public GameObject player;
	public Text title;
	public Scene scene;
	bool pressedTrigger = false;
	Vector3 pos;

	public void closeCanvas(){
		SceneTitleCanvas.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene();
		//fetch the instruction text from GlobalVariables.cs
		title.text = GlobalVariables.SceneInstructions[scene.name];
		//fetch the player's position
		pos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//if the trigger buttons are pressed, release the player
		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)){
			pressedTrigger = true;
		}
		if(!pressedTrigger)
		{
			player.transform.position = pos;
		}
		else{
			closeCanvas();
			return;
		}
	}
}
