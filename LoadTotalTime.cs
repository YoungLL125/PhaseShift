using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadTotalTime : MonoBehaviour
{
    public TMP_Text timeText;
    void Start()
    {
        timeText.text = Stopwatch.totalTime.ToString("0.00");
        // Enables the cursor and make it visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; 
    }
}
