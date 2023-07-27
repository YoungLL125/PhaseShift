using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public static int stage; // Tracks current stage of player

    void Start()
    {
        stage = SceneManager.GetActiveScene().buildIndex; // Current stage of player
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player"){
            // Change to transition scene
            Stopwatch.timing = false; // Stops the stopwatch
            Stopwatch.allTimes[stage] = Stopwatch.currentTime; // Records value in the array
            SceneManager.LoadScene("Transition"); // Load transition scene
        }
    }
}
