using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    public RaycastHit hit;
    public string objtag;
    private LineRenderer lr;
    private Vector3 grapplePos;
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
        if (Input.GetMouseButtonDown(0)){
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0)){
            StopGrapple();
        }
        else if (dimScript.currentDim != objtag && objtag != "Untagged"){
            StopGrapple();
        }
        else if (Input.GetMouseButton(0)){
            // Double check if mouse button held down; Do nothing
        }
        else{
            StopGrapple();
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

            objtag = hit.collider.gameObject.tag;

            print(objtag);

            grapplePos = hit.point; // Stores hit location into grapplePos
            
            joint = player.gameObject.AddComponent<SpringJoint>(); // Creates a springjoint component in player gameobject
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePos; // Sets one end of the spring joint to the target location

            float distance = Vector3.Distance(player.position, grapplePos); // Distance between player and grapple point

            // The distance range that the grapple will try to keep within
            joint.maxDistance = distance * 0.9f; // Max distance
            joint.minDistance = distance * 0.25f; // Min distance

            joint.spring = spring; // Strength of spring
            joint.damper = damper; // Amount that the spring is reduced when active
            joint.massScale = massScale; // The player's mass divider

            lr.positionCount = 2;
        }
    }

    void StopGrapple(){
        lr.positionCount = 0;
        joints = player.GetComponents<SpringJoint>();
        foreach (SpringJoint j in joints){
            Destroy(j);
        }
    }

    void DrawRope(){

        if (!joint) return;

        // Sets the start and end points of the rendered line
        lr.SetPosition(0,gunTip.position);
        lr.SetPosition(1,grapplePos);
    }
}
