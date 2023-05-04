using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial1 : MonoBehaviour
{
    public TMP_Text txt;
    public GameObject help;
    public int dispVal = 0;
    public bool complete = false;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = "Move with WASD";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && complete == false){
            dispVal += 1;
            if (dispVal == 1){
                txt.text = "Hold down shift to sprint";
            }
            else if (dispVal == 2){
                txt.text = "Press F to switch dimensions";
            }
            else if (dispVal == 3){
                txt.text = "Press space to jump";
            }
            else if (dispVal == 4){
                txt.text = "Hold down left click to grapple";
            }
            else if (dispVal == 5){
                txt.text = "Spam left click to reel yourself in";
            }
            else if (dispVal == 6){
                help.SetActive(false);
                complete = true;
            }
        }
    }
}
