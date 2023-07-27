using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsMM : MonoBehaviour
{
    public Button startButton, settingsButton;
    void Start()
    {
        // Enable cursor and set to visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 

        Finish.stage = 0; // Scene number of the first level

        // Button Listener
        startButton.onClick.AddListener(Init);
        settingsButton.onClick.AddListener(TransitionButtons.Settings);
    }

    void Init()
    {
        // Load first scene (2) when start button is clicked
        SceneManager.LoadScene(Finish.stage);
    }
}
