using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestscrLoadTestLevel : MonoBehaviour
{
    [Tooltip("this should be 2 (if this is a build) or 0 (if this is a developersbuild)")]
    public int theCurrentLevel = 2;

    public void BackToStart()
    {
        SceneManager.LoadScene(theCurrentLevel);
    }

}
