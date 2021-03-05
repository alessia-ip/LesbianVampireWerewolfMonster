using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemAndWorldParser : MonoBehaviour
{
    //This script is to store all the important dialogue for the scene
    [System.Serializable]
    public class WorldTextInstance //This is a class specifically to hold dialogue
    {
        public string text;
        public bool isItem;
        public string itemName;

        public WorldTextInstance(string txt, bool type, string itNm)
        {
            text = txt;
            isItem = type;
            itemName = itNm;
        }
    }

    public string FILE_SCRIPT; //File Name formatted as -> name.txt || DO NOT USE A / in the inspector view
    private const string DIR = "/Resources/DialogueAndTextFiles/"; //The directory path
    private string FILE_PATH; //the file path
    private string readText; //the text read out of the file

    [FormerlySerializedAs("_dialogueInstances")] public List<WorldTextInstance> _worldTextInstances= new List<WorldTextInstance>(); //all instances of dialogue in the scene

    
    void Start()
    {
        //get the application path 
        FILE_PATH = Application.dataPath + DIR + FILE_SCRIPT;
        //read all text from the file
        readText = File.ReadAllText(FILE_PATH);
        //split the file into an array on every new line
        string[] lines = readText.Split('\n');


        for (int i = 0; i < lines.Length; i++)
        {
            // - 1:text text text text text:true:Item (item format)
            // - 2:text text text text:false:Fireplace (world format)

            if (!lines[i].Contains("***")) //lines with *** should just be headers/titles
            {
                string[] parsedParts = lines[i].Split(':'); //split on the colon
                if (parsedParts.Length > 1)
                {
                    if (Boolean.Parse(parsedParts[2])) //this works for items!
                    {
                        _worldTextInstances.Add(new WorldTextInstance(parsedParts[1], Boolean.Parse(parsedParts[2]), parsedParts[3]));
                    }
                    else
                    {
                        _worldTextInstances.Add(new WorldTextInstance(parsedParts[1], Boolean.Parse(parsedParts[2]), parsedParts[3]));
                        //WE KEEP THE NAME even for world items so I can do text formatting with it later. 
                    }
                    
                }

            }

        }

    }
    
    

}
