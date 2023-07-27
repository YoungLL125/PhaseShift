using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionButtons : MonoBehaviour
{
    public Button continueButton, retryButton;
    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(LoadNext);
        retryButton.onClick.AddListener(Retry);
    }

    void LoadNext()
    {
        SceneManager.LoadScene(Finish.stage + 1);
    }
    void Retry()
    {
        SceneManager.LoadScene(Finish.stage);
    }
}
