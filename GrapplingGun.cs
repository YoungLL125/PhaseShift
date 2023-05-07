using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    public RaycastHit hit;
    public string objtag;
    private LineRenderer lr;
    private Vector3 localgrapplePos;
    public Transform gunTip, player, cam;
    public float maxDist;
    public float spring;
    public float damper;
    public float massScale;
    private SpringJoint joint;
    private SpringJoint[] joints;
    public SwitchDim dimScript;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        dimScript = GameObject.Find("Player").GetComponent<SwitchDim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ // Start grapple when left click is pressed
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0)){ // Stop grapple when left click is released
            StopGrapple();
        }
        else if ((objtag == "DIM0" || objtag == "DIM1") && objtag != dimScript.currentDim){ // Stop grapple when the object is a dimension object AND the dimension of that object does not match the current dimension
            StopGrapple();
        }
        else if (Input.GetMouseButton(0)){
            // Double check if mouse button held down; Do nothing
        }
        else{
            StopGrapple(); // Stop grapple as the mouse button is not held down
        }
    }

    void LateUpdate(){
        // Draw rope after the update function
        DrawRope();
    }

    void StartGrapple(){
        // Uses a springjoint component to mimick the grappling hook effect
        // Sends a raycast to find target location
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDist, 1)){

            objtag = hit.collider.gameObject.tag; // The object tag of the connected object
            
            // Prevents grappling to yourself
            if (objtag != "Player"){
                localgrapplePos = hit.transform.InverseTransformPoint(hit.point); // The position of the hitpoint relative to the hit object
            
                joint = player.gameObject.AddComponent<SpringJoint>(); // Creates a springjoint component in player gameobject
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedBody = hit.rigidbody; // Set the connected body of the joint to the rigidbody that is hit
                joint.connectedAnchor = localgrapplePos; // Sets one end of the spring joint to the target location

                float distance = Vector3.Distance(player.position, hit.point); // Distance between player and global grapple point

                // The distance range that the grapple will try to keep within
                joint.maxDistance = distance * 0.9f; // Max distance
                joint.minDistance = distance * 0.25f; // Min distance

                joint.spring = spring; // Strength of spring
                joint.damper = damper; // Amount that the spring is reduced when active
                joint.massScale = massScale; // The player's mass divider

                lr.positionCount = 2; // two endpoints, (start, end)
            }
            else{
                Invoke("StartGrapple", 0.05f);
            }
        }
    }

    void StopGrapple(){
        lr.positionCount = 0; // zero endpoints
        joints = player.GetComponents<SpringJoint>(); // Destroy all springjoints
        foreach (SpringJoint j in joints){
            Destroy(j);
        }
    }

    void DrawRope(){

        if (!joint) return;

        // Sets the start and end points of the rendered line
        lr.SetPosition(0,gunTip.position); // Guntip position
        lr.SetPosition(1,joint.connectedBody.transform.TransformPoint(joint.connectedAnchor)); // The global position of the connected anchor
    }
}
