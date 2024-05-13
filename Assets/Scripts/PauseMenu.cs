using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPause = false;
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        IsPause = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Pause ()
    {
        IsPause = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
