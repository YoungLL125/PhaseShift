using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadTime : MonoBehaviour
{
    public TMP_Text timeText;
    void Start()
    {
        timeText.text = Stopwatch.currentTime.ToString("0.00");
    }
}
