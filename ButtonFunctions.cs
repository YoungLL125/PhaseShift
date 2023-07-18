using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public Button startButton, settingsButton;
    void Start()
    {
        // Enable cursor and set to visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 

        Finish.stage = 2; // Scene number of the first level

        // Add listener to button
        startButton.onClick.AddListener(Init);
    }

    void Init()
    {
        // Load first scene (2) when start button is clicked
        SceneManager.LoadScene(Finish.stage);
    }


}
