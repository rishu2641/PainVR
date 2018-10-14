using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WalkingThenStop : MonoBehaviour {

	public float speed = 10;
	float posX = 0;
	float posY = 0;
	float posZ = 0;

	// Use this for initialization
	void Start () {
		posX=transform.position.x;
		posY=transform.position.y;
		posZ=transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		float distanceX = transform.position.x - posX;
		float distanceY = transform.position.y - posY;
		float distanceZ = transform.position.z - posZ;

		float distance = Mathf.Sqrt(distanceX*distanceX + distanceY*distanceY + distanceZ*distanceZ);


		if(distance < 14){
		 transform.Translate(speed* Vector3.forward * Time.deltaTime);
		}
	}
}

