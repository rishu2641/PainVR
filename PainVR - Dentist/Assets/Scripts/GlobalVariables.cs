using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


/*

Hello future developers on OPAL Testing. First off, our POs were excellent throughout the entire process
so you definitely got paired with a great project. Look, I'm going to give it to you straight, we had 4 team members
in total throughout the semester. Only two of us actually worked on the project. It was disappointing to say the least,
so progress was limited and we didn't accomplish as much as we thought we would. Due to the lack of teammates, we found
ourselves working long hours the night before sprint deadlines so as to make up functionality our other teammates promised they would finish.
So code may be sloppy, and we just didn't have time to refactor. We apologize in advance, it's really not too bad I promise.

For the most part, nothing is hardcoded, and a lot of general functionality can be accomplished by simply making changes to this file. 
Like, if you want to add a scene you built to the experience, just add the scene name to the array below (ith instructions in the dictionary below).

The main files with functionality are:

*GlobalVariables.cs (you are here)
*GlobalFunctions.cs (static class with useful functions)
*DisplayTitle.cs (started as the script to alert the scenes instructions, but (d)evolved into the entire timing logic for transitioning scenes)
*ToggleSlider.cs (slider functionality)

and that's basically it. There are a few files here and there you may come across that you need, but for the most part all of what we did is in those files.

If you built a scene and you wish to add it to the shuffle, make sure it's added to your build settings, and just add it to the relevant arrays/dictionaries below.

I may add to this at some point this summer ('18), but if not, I guess this is it. Please resist the urge to insult our code. If you need me, hit me up.

*/

public static class GlobalVariables
{

    //Initialize array of scenes here:
   public static string[] scenes = {"apple-1-supermarket", "apple-2-supermarket", "apple-3-cafeteria", "apple-4-cafeteria", "apple-5-cafeteria","apple-6-cafeteria"};

    //strPath holds path to user's desktop
    private static string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

    //filename of patient's shuffled .csv
    private static string filename = strPath + "\\Patient_Default_Shuffled_OPAL_Testing.csv";

    //filename of patient's save .txt
    public static string savefilename = strPath + "\\default_save.txt";
    
    //patient name
    private static string patientname;

    //Sample rate for records in patient file
    private static float sampleRate = 2.0f; 

    //How long anxiety level needs to be below threshold before transitioning to next scene.
    private static int timeTillNextScene = 5;

    //scene instruction hash map. Keys are scene names, values are instructions to display.
    public static Dictionary<string, string> SceneInstructions = new Dictionary<string, string>
        {
            { "apple-1-supermarket", "You walk into the supermarket to start your grocery shopping. Look for the produce aisle!" },
            { "apple-2-supermarket", "You've found yourself in the produce aisle. Feel free to approach the apples and gauge your anxiety level." },
            { "apple-3-cafeteria", "Welcome to the cafeteria. Take a look at what's for lunch!" },
            { "apple-4-cafeteria", "Good to see your coworkers eating healthy. How do you feel about their choice?" },
            { "apple-5-cafeteria", "You're sitting down to eat with your coworker. Hope you don't mind the crunch!" },
            { "apple-6-cafeteria", "Engaged in conversation with your coworker, you decide to eat your apple." },
        };

    //global flag that represents whether or not the user has finished Tutorial.cs
    public static bool tutorialDone = false;

    //global flag that represents patient has existing save
    public static bool isExisting = false;

    //array that holds scene order for existing user
    public static string[] existingSceneOrder = new string[scenes.Length];

    //slider threshold to proceed to next scene for existing users
    public static float existingThreshold = 15.0f;

    //exact time when user enters first scene of experience
    public static double startTime = 0;

    //list that ranks scenes in order of least anxious -> most anxious by final anxiety level
    public static Dictionary<string, float> scenesRank = new Dictionary<string, float>();

    //value of slider
    public static float sliderValue = 0f;
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