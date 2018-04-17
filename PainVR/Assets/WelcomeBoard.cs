using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeBoard : MonoBehaviour {

	public Transform welcomeBoard;

	public float movementSpeed = 10;

	public float transitionTimes = 3;

	float doneTime = 0;

	bool done;

	// Use this for initialization
	void Start () {
		done = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(done && Time.time - doneTime >= transitionTimes){
			SceneManager.LoadScene("Tutorial");
		}
		if(Time.time < transitionTimes){

		}
		else if(welcomeBoard.position.y >= 11.5 ){
			if(!done){
				doneTime = Time.time;
				done = true;
			}
		}
		else{
			welcomeBoard.Translate(Vector3.up * movementSpeed * Time.deltaTime);	
		}
		
	}
}
