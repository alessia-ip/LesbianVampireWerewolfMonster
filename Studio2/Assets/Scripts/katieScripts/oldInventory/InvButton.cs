using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InvButton : MonoBehaviour
{
    private const string DIR = "/Logs";
    private const string FILE_COMBOS = DIR + "/CorrectCombosFile.txt";
    private string FILE_PATH_COMBO;

    public List<string> comboList;
    [HideInInspector]
    public string itemType = "Nothing";

    private void Start()
    {
        FILE_PATH_COMBO = Application.dataPath + (FILE_COMBOS);
        SplitComboFile();
    }

    void SplitComboFile()
    {
        string[] fileData = File.ReadAllLines(FILE_PATH_COMBO);
        for (int i = 0; i < fileData.Length; i++)
        {
            comboList.Add(fileData[i]);
        }
    }

    public void HoldClick()
    {
        switch (itemType)
        {
            case "Cigar":
                Inventory.instance.HeldItem = "Cigar";
                break;
            case "Book":
                Inventory.instance.HeldItem = "Book";
                break;
            case "Paper":
                Inventory.instance.HeldItem = "Paper";
                break;
            case "Nothing":
                Inventory.instance.HeldItem = "Nothing";
                break;
        }

        Inventory.instance.heldImage.sprite = this.GetComponentInParent<Image>().sprite;
    }

    public void CombineCLick()
    {
        string held = Inventory.instance.HeldItem;//heldItem
        string combine = itemType; //item that combine with held
        string heldPlusCombine = held + combine;
        string combinePlusHeld = combine + held;

        for (int i = 0; i < comboList.Count; i++)
        {
            if (heldPlusCombine == comboList[i] || combinePlusHeld == comboList[i])
            {
                Debug.Log("ya did it kiddo");
            }
        }
    }
}