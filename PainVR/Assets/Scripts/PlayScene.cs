using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour {
    public GameObject camera;
    public GameObject canvas;
	// Use this for initialization

	void openMenu(){
		 camera = GameObject.Find("Main Camera");
	     camera.GetComponent<CameraScript>().enabled = false;
	     canvas.SetActive(true);
	}
	void Start () {
		canvas = GameObject.Find("Interface");
        canvas.SetActive(false);
		StartCoroutine(ExecuteAfterTime(15));	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator ExecuteAfterTime(float time){
	    yield return new WaitForSeconds(time);
	 	openMenu();         

	 }
}
