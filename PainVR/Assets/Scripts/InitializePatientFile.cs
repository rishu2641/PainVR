using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;



public class InitializePatientFile : MonoBehaviour {

	public InputField iField;
	public string patientName;
	public string patientFile;

	public void StartPatientFile(){
		patientName = iField.text;
		if(patientName.Trim() != ""){
			patientName = patientName.Trim().Replace(" ", "_");
			string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);
	 		patientFile = strPath + "\\Patient_" + patientName + "_Shuffled_OPAL_Testing.csv";
	 		GlobalVariables.Filename = patientFile;
	 		GlobalVariables.Patientname = patientName;
	 		System.IO.File.WriteAllText(patientFile, 
	 									"Patient's Name:,"+patientName+",,,\n" + ",,,,\n" + "Time,Scene,Anxiety Level,Scene, Average Anxiety Level");
			
			SceneManager.LoadScene("apple-1-supermarket");
		}
	}
}
