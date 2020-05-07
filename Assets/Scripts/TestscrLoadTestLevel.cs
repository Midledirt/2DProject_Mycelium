using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestscrLoadTestLevel : MonoBehaviour
{
    public void BackToStart()
    {
        SceneManager.LoadScene("Level1");
    }

}
