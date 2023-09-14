using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsGO : MonoBehaviour
{
    public Button playagainButton;
    void Start()
    {
        // Enable cursor and set to visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 

        // Button Listener
        playagainButton.onClick.AddListener(Replay);
    }

    void Replay()
    {
        // Reset everything for player to play again
        Stopwatch.totalTime = 0;

        // Load the Main Menu scene
        SceneManager.LoadScene(0);
    }
}
