using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;



public class InitializePatientFile : MonoBehaviour {

	public InputField iField;
	public string patientName;

	public void StartPatientFile(){
		patientName = iField.text;
		if(patientName.Trim() != ""){
			patientName = patientName.Trim().Replace(" ", "_");
			string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);
	 		System.IO.File.WriteAllText(strPath + "\\Patient_" + patientName + "_Opal_Testing.csv", "Patient's Name:,"+patientName+",\n" + ",,\n" + "Scene,Time,Anxiety Level\n");
			SceneManager.LoadScene("Tutorial");
		}
	}
}
