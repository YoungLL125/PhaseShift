using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionButtons : MonoBehaviour
{
    public Button continueButton;
    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(LoadNext);
    }

    void LoadNext()
    {
        SceneManager.LoadScene(Finish.stage + 1);
    }
}
