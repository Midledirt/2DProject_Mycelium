using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrBackToMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
