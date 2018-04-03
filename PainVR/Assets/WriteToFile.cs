using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;


public class WriteToFile : MonoBehaviour {

	public void WriteFile(){
		 System.IO.File.WriteAllText("C:\\Users\\Joe\\Desktop\\yourtextfile.csv", "Does this work?");
	}
}
