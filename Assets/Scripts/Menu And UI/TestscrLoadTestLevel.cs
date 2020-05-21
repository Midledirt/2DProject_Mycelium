using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestscrLoadTestLevel : MonoBehaviour
{
    public int theCurrentLevel = 2;


    public void BackToStart()
    {
        SceneManager.LoadScene(theCurrentLevel);
    }

}
