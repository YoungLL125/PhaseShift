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
            SceneManager.LoadScene("Transition");
        }
    }
}
