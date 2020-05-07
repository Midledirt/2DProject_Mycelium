using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrText : MonoBehaviour
{

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;

    //This decides what text to open. Set to the first text by default.
    public int textToDisplay = 1;

    public bool isTouched;

    // Start is called before the first frame update
    void Start()
    {
        isTouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched == true)
        {
            displayText(textToDisplay);
        }
    }


    public void displayText(int textToDisplay)
    {
        switch (textToDisplay)
        {
            case 0:
                text1.SetActive(true);
                Time.timeScale = 0f;
                break;
            case 1:
                text2.SetActive(true);
                Time.timeScale = 0f;
                break;
            case 2:
                text3.SetActive(true);
                Time.timeScale = 0f;
                break;
            case 3:
                text4.SetActive(true);
                Time.timeScale = 0f;
                break;
            default:
                print("Theres something wrong with the switch text function");
                break;
        }
    }
    public void closeText()
    {
        isTouched = false;
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        Time.timeScale = 1f;
    }

        /*public void closeText(int textToDisplay)
        {
            switch (textToDisplay)
            {
                case 0:
                    text1.SetActive(false);
                    Time.timeScale = 1f;
                    break;
                case 1:
                    text2.SetActive(false);
                    Time.timeScale = 1f;
                    break;
                case 2:
                    text3.SetActive(false);
                    Time.timeScale = 1f;
                    break;
            }
        }*/

    }
