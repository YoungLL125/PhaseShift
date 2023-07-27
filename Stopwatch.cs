using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public TMP_Text timeText;
    public static float currentTime;
    public static float totalTime;
    public static float[] allTimes = new float[50]; // Initialise time array with 50 elements
    public static bool timing;
    void Start()
    {
        timing = true;
        currentTime = 0f;
    }
    void Update()
    {
        if (timing){
            currentTime += Time.deltaTime;
            timeText.text = currentTime.ToString("0.00");
        }
    }
}
