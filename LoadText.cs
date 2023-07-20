using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadText : MonoBehaviour
{
    public Canvas help;    
    public TMP_Text txt;
    public string lvl;
    void Start()
    {
        help.enabled = true;

        lvl = SceneManager.GetActiveScene().name;
        
        if (lvl == "Lvl0"){
            txt.text = "Press 'F' to switch between dimensions";
        }
        if (lvl == "Lvl1"){
            txt.text = "Press spacebar to jump";
        }
        if (lvl == "Lvl2"){
            txt.text = "Hold down left click to grapple";
        }
        if (lvl == "Lvl3"){
            txt.text = "Weeeeee!!!";
        }
        if (lvl == "Lvl4"){
            txt.text = "Learn to conserve your momentum";
        }
        if (lvl == "Lvl5"){
            txt.text = "Try not to get dizzy";
        }
        if (lvl == "Lvl6"){
            txt.text = "Platforms can move too";
        }
        if (lvl == "Lvl7"){
            txt.text = "Ouch! It hurts!";
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit")){
            if (help.enabled == true){
                help.enabled = false; // Hides the help canvas
            }
        }
    }
}
