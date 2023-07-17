using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public TMP_Text timeText;
    public static float currentTime;
    void Start()
    {
        
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        timeText.text = currentTime.ToString("0.00");
    }
}
