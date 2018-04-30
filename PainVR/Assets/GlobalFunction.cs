using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/* This class holds all excel-file logging functionality */
public class GlobalFunction {
	public static string coma = ",";

    public static void LogToPatientFile(string fileLocation, string sceneName, string time, float anxiety) {
        string row = "\n" + time + coma + sceneName + coma + anxiety + coma + coma; 
        System.IO.File.AppendAllText(fileLocation, row);
    }

   
}