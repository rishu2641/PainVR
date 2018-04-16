using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public static class GlobalVariables
{
	private static string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);
    private static string filename = strPath + "\\Patient_Default_Opal_Testing.csv";
    private static string patientname;
    public static string Filename 
    {
        get 
        {
            return filename;
        }
        set 
        {
            filename = value;
        }
    }
    public static string Patientname 
    {
        get 
        {
            return patientname;
        }
        set 
        {
            patientname = value;
        }
    }
    public static bool updatePatientFile(){
    	
    	return true;
    }
}