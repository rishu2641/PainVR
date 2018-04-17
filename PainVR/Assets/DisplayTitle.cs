using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTitle : MonoBehaviour {

	public GameObject SceneTitleCanvas;

	public void closeCanvas(){
		SceneTitleCanvas.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		Invoke("closeCanvas", 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
