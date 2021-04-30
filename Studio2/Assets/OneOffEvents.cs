using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneOffEvents : MonoBehaviour
{

    public TextMeshProUGUI dialogueCheck;

    public DialogueScriptableObject mapGetDialogue;
    public GameObject mapButton;
    public GameObject mapCollider;

    // Update is called once per frame
    void Update()
    {
        if (dialogueCheck.text == mapGetDialogue.altDialogue)
        {
            mapButton.SetActive(true);
            mapCollider.SetActive(true);
        }
    }
}
