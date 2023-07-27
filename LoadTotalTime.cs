using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadTotalTime : MonoBehaviour
{
    public TMP_Text timeText;
    public float mins;
    public float secs;
    void Start()
    {
        // Add the time of the last level
        Stopwatch.totalTime += Stopwatch.currentTime;

        // Structure the display of time by mins,secs,milisecs
        mins = Mathf.Floor(Stopwatch.totalTime/60);
        secs = Stopwatch.totalTime % 60;
        timeText.text = mins.ToString("00") + ":" + secs.ToString("00.00");
        
        // Enables the cursor and make it visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 
    }
}
