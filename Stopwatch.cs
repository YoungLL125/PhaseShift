using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public TMP_Text timeText;
    public static float currentTime;
    public static float totalTime;
    void Start()
    {
        totalTime += currentTime;
        currentTime = 0f;
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        timeText.text = currentTime.ToString("0.00");
    }
}
