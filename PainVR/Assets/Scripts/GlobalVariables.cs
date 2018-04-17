using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public static class GlobalVariables
{
    //Initialize array of scenes here:
   public static string[] scenes = {"Tutorial", "apple-1-supermarket", "apple-2-supermarket", "apple-3-cafeteria", "apple-4-cafeteria", "apple-5-cafeteria","apple-6-cafeteria"};

    //strPath holds path to user's desktop
    private static string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

    //filename of patient's shuffled .csv
    private static string filename = strPath + "\\Patient_Default_Shuffled_OPAL_Testing.csv";
    
    //patient name
    private static string patientname;

    //Sample rate for records in patient file
    private static float sampleRate = 2.0f; 

    //How long anxiety level needs to be below threshold before transitioning to next scene.
    private static int timeTillNextScene = 5;

    //call GlobalVariables.Filename to return path+filename
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

    //call GlobalVariables.Patientname to return patient's full name.
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

    //call GlobalVariables.Scenes to return array of scenes
    public static string[] Scenes 
    {
        get 
        {
            return scenes;
        }
        set 
        {
            scenes = value;
        }
    }

    //Call GlobalVariables.SampleRate to return sampleRate
    public static float SampleRate 
    {
        get 
        {
            return sampleRate;
        }
        set 
        {
            sampleRate = value;
        }
    }

    public static int TimeTillNextScene
    {
        get 
        {
            return timeTillNextScene;
        }
        set 
        {
            timeTillNextScene = value;
        }
    }
}