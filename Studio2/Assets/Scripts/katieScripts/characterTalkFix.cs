using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterTalkFix : MonoBehaviour
{
    public GameObject Akari;
    public GameObject Gardener;
    public GameObject MxPaws;

    public List<GameObject> turnOn = new List<GameObject>();
    public List<GameObject> turnOff = new List<GameObject>();

    private void Start()
    {
        Gardener.SetActive(false);
        MxPaws.SetActive(false);
    }

    public void OnLocSelect(string place)
    {
        turnOn.Clear();
        turnOff.Clear();
        switch (place)
        {
            case "Garden":
                turnOn.Add(Gardener);
                turnOff.Add(Akari);
                turnOff.Add(MxPaws);
                break;
            case "Chapel":
                turnOn.Add(Akari);
                turnOff.Add(MxPaws);
                turnOff.Add(Gardener);
                break;
            case "Barracks":
                turnOn.Add(MxPaws);
                turnOff.Add(Gardener);
                turnOff.Add(Akari);
                break;
        }
    }

    public void OnYesSelect()
    {
        foreach (var gameObject in turnOn)
        {
            gameObject.SetActive(true);
        }

        foreach (var gameObject in turnOff)
        {
            gameObject.SetActive(false);
        }
    }
}
