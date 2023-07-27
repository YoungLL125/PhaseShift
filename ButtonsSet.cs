using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsSet : MonoBehaviour
{
    public Button returnButton;
    void Start()
    {
        // Enable cursor and set to visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 
        
        // Button Listener
        returnButton.onClick.AddListener(Return);
    }
    void Return()
    {
        SceneManager.LoadScene(ButtonsTrans.prevScene);
    }
}
