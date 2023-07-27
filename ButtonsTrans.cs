using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsTrans : MonoBehaviour
{
    public Button continueButton, retryButton, settingsButton;
    public static int prevScene; // Stores previous scene
    void Start()
    {
        continueButton.onClick.AddListener(LoadNext);
        retryButton.onClick.AddListener(Retry);
        settingsButton.onClick.AddListener(Settings);
    }

    void LoadNext()
    {
        SceneManager.LoadScene(Finish.stage + 1);
    }
    void Retry()
    {
        SceneManager.LoadScene(Finish.stage);
    }
    public static void Settings() // Global function for every instance when entering settings
    {
        prevScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Settings");
    }
}
