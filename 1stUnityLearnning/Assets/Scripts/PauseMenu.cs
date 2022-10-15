using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    bool inPauseMenu;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    private void Start()
    {
        Application.targetFrameRate = 120;
    }

    void Update()
    {
        if(Input.GetKeyDown (KeyCode.Escape))
        {
            if(isPaused && inPauseMenu)
            {
                Resume();
            }
            else
            {
                Pause();
                if (!inPauseMenu)
                {
                    QuitOptions();
                }
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void QuitOptions()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        inPauseMenu = true;
    }
    public void EnterOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        inPauseMenu = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
