using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class changeTextIfExisting : MonoBehaviour {

	public Text goodbye;

	// Use this for initialization
	void Start () {
		if(GlobalVariables.isExisting){
			goodbye.text = "You have completed the final section of the OPAL testing process.\n\nYou may now remove the headset and wait for further instruction.\n\nThank you for participating!";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
