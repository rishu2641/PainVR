using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurnchingApple : MonoBehaviour {
	public GameObject head;
	public bool yes = false;
	// Update is called once per frame
	void Update () {
		
		float posX = head.transform.position.x;
		float posY = head.transform.position.y-.1f;
		float posZ = head.transform.position.z;

		float distanceX = posX-transform.position.x;
		float distanceY = posY-transform.position.y;
		float distanceZ = posZ-transform.position.z;

		float distance = Mathf.Sqrt(distanceX*distanceX - distanceY*distanceY - distanceZ*distanceZ);

		if(distance < .2){
			yes = true;
			AudioSource audio = GetComponent<AudioSource>();
        	audio.Play();
		}
		else{yes =false;}
        

        
           
    
	}
}
