using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;
    public static bool GameisPaused = false;
    public Button resumeButton,retryButton,settingsButton;
    void Start()
    {
        // Disable Pause Menu
        pauseMenu.enabled = false;

        // Button Listeners
        resumeButton.onClick.AddListener(Resume);
        retryButton.onClick.AddListener(Retry);
        settingsButton.onClick.AddListener(ButtonsTrans.Settings); // Refer to global settings function
    }
    void Update()
    {
        // Run pause function only when game is not paused
        if (Input.GetKeyDown(KeyCode.P)){
            if (GameisPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    void Resume()
    {
        // Lock cursor and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

        pauseMenu.enabled = false; // Disable menu
        Time.timeScale = 1f; // Continue Time
        GameisPaused = false;
    }
    void Pause()
    {
        // Enable cursor and set to visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 

        pauseMenu.enabled = true; // Enable menu
        Time.timeScale = 0f; // Stop Time
        GameisPaused = true;
    }
    
    void Retry()
    {
        Resume();
        SceneManager.LoadScene(Finish.stage); // Reload stage
    }
}
