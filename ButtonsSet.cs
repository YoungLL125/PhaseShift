using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsSet : MonoBehaviour
{
    public Button returnButton;
    public Slider MouseSensiSlider, FovSlider;
    void Start()
    {
        // Enable cursor and set to visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 

        // Load settings
        MouseSensiSlider.value = Move.sensitivity;
        FovSlider.value = Move.fov;
        
        // Button Listener
        returnButton.onClick.AddListener(Return);
    }
    void Return()
    {
        // Apply settings
        Move.sensitivity = MouseSensiSlider.value;
        Move.fov = FovSlider.value;

        // Load previous scene
        SceneManager.LoadScene(ButtonsTrans.prevScene);
    }
}
