using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDim : MonoBehaviour
{
    public GameObject[] dim0;
    public GameObject[] dim1;
    public string currentDim = "DIM1";
    public Material day;
    public Material night;

    void Start()
    {
        dim0 = GameObject.FindGameObjectsWithTag("DIM0");
        dim1 = GameObject.FindGameObjectsWithTag("DIM1");
        
        SwitchOff(dim0);
        SwitchOn(dim1);
        RenderSettings.skybox = day;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if shift is pressed
        if (Input.GetButtonDown("F")){
            if (currentDim == "DIM1"){
                currentDim = "DIM0";
                // Changes all objects in Dimension 1 to translucent
                SwitchOff(dim1);
                // Changes all objects in Dimension 0 to opaque
                SwitchOn(dim0);
                // Changes skybox
                RenderSettings.skybox = night;
            }
            else if (currentDim == "DIM0"){
                currentDim = "DIM1";
                // Changes all objects in Dimension 0 to translucent
                SwitchOff(dim0);
                // Changes all objects in Dimension 1 to opaque
                SwitchOn(dim1);
                // Changes skybox
                RenderSettings.skybox = day;
            }
        }


    }

    void SwitchOn(GameObject[] dimension){
        // Runs through every object in the array dimension
        foreach (GameObject obj in dimension){
            // Can interact with obj
            Collider[] colliders = obj.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders){
                collider.enabled = true;
            }
            Color col; // Sets a color variable to use
            col = obj.GetComponent<Renderer>().material.color; // Get the color of the material of the Renderer component of the obj 
            col.a = 1f; // Sets transparency to opaque (0 transparent, 1 opaque)
            obj.GetComponent<Renderer>().material.color = col; // Apply to the obj
        }
    }

    void SwitchOff(GameObject[] dimension){
        // Runs through every object in the array dimension
        foreach (GameObject obj in dimension){
            // Cannot interact with obj
            Collider[] colliders = obj.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders){
                collider.enabled = false;
            }
            Color col; // Sets a color variable to use
            col = obj.GetComponent<Renderer>().material.color; // Get the color of the material of the Renderer component of the obj
            col.a = 0.1f; // Sets to translucent
            obj.GetComponent<Renderer>().material.color = col; // Apply to the obj
        }
    }
}
