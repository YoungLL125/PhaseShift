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
        startButton.onClick.AddListener(Init);
    }

    void Init()
    {
        SceneManager.LoadScene(1);
    }


}
