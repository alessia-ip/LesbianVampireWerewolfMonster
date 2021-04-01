using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvHover : MonoBehaviour
{
    Button holdButton;
    Button combineButton;
    private InvButton testInvType;

    private void Start()
    {
        testInvType = this.gameObject.GetComponent<InvButton>();
        holdButton = this.gameObject.transform.GetChild(0).GetComponent<Button>();
        combineButton = this.gameObject.transform.GetChild(1).GetComponent<Button>();
    }

    public void HoverEnter()
    {
        if (testInvType.itemType != "Nothing")
        {
            holdButton.gameObject.SetActive(true);
            combineButton.gameObject.SetActive(true);
        }

    }

    public void HoverExit()
    {
        holdButton.gameObject.SetActive(false);
        combineButton.gameObject.SetActive(false);
    }
}