using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    
    //This script is to store all the important dialogue for the scene
    [System.Serializable]
    public class DialogueInstance //This is a class specifically to hold dialogue
    {
        public string character;
        public string dialogue;
        public string portrait;
        public string[] replies;

        public DialogueInstance(string chara, string dia, string port, string[] rep)
        {
            character = chara;
            dialogue = dia;
            portrait = port;
            replies = rep;
        }
    }
    


    
    public string FILE_DIALOGUE; //File Name formatted as -> name.txt || DO NOT USE A / in the inspector view
    private string FILE_PATH; //the file path
    
    public List<DialogueInstance> _dialogueInstances= new List<DialogueInstance>(); //all instances of dialogue in the scene
    
    // Start is called before the first frame update
    void Start()
    {
        
  
        String GET_DIALOGUE = FILE_DIALOGUE;
        TextAsset dialogueTxt = Resources.Load<TextAsset>(GET_DIALOGUE); //YOU MUST LOAD FROM RESOURCES - DO NOT USE .txt IN THE EDITOR (check first if this breaks)
        string[] lines = dialogueTxt.text.Split('\n');
            
  
        for(int i = 0; i < lines.Length; i++)
        {
              if (!lines[i].Contains("***"))//lines with *** should just be headers/titles
              {
                string[] parsedParts = lines[i].Split(':'); //split on the colon

                if (parsedParts.Length > 1)
                {
                    //this is any replies 
                    string[] answers = null; //we set to null in case there are no replies (and nothing to access)
                    if (parsedParts.Length == 5)
                    {
                        //this only happens if there are replies
                        //we split replies on the pipe symbol!
                        answers = parsedParts[4].Split('|');
                    }
                    
                    _dialogueInstances.Add(new DialogueInstance(parsedParts[1], parsedParts[2], parsedParts[3], answers)); 
                    //0 would be the line number, which we don't want
                    //1 is the next piece of info - the character
                    //2 is the actual line of dialogue
                    //3 is the portrait type
                    //Debug.Log(_dialogueInstances[0].replies[0]);

                } 
              }
        }

    }
}
