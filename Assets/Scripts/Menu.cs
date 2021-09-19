using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static string theName;
    public GameObject inputField;
    public GameObject textDisplay;

    void Start()
    {
        //GameManager.gameManager.Save();
    }

    void Update()
    {
        
    }

    public void StartRun()
    {
        GameManager.instance.StartRun();
    }

    public void EnterYourName()
    {
        theName = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Start your run, " + theName + "! :)";

    }
}
