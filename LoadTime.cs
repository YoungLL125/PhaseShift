using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadTime : MonoBehaviour
{
    public TMP_Text timeText;
    public float mins;
    public float secs;
    void Start()
    {
        mins = Mathf.Floor(Stopwatch.currentTime/60);
        secs = Stopwatch.currentTime % 60;
        timeText.text = mins.ToString("00") + ":" + secs.ToString("00.00");
        
        // Enables the cursor and make it visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 
    }
}
