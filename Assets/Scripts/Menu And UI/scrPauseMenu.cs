using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrPauseMenu : MonoBehaviour
{
    //Notes 2 self:
    //In order 2 continnue with this, I need 2 make it possible to call the function of pauseing or resuming at any point (so it must be placed in the )




    public bool PauseButtonPressed = false;
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    /*public void Update()
    {
        if (PauseButtonPressed == true)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }*/

    //This function is called from a button placed in the game UI
    //(since we have no escape button to use on a mobile phone)
    public void PauseButton()
    {
        Pause();
    }
    //This function is called from a button placed on the pause menu itself.
    public void ResumeButton()
    {
        Resume();
    }
    public void OpenSettings()
    {
        //Opens settings menu by disabling the main menu, and activating the settings menu
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    public void BackButton()
    {
        //Opens settings menu by disabling the settings menu, and activating the main menu
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

  

    public void QuitGame()
    {
        print("Quitting game (thiw would work if this was an actual build)");
        Application.Quit();
    }
}
