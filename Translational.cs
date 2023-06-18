using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translational : MonoBehaviour
{
    public GameObject[] waypoints;
    private int num = 0;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if the position of the platform is at the position of the current waypoint
        if (transform.position == waypoints[num].transform.position){
            // If the value  of the waypoint is the last element of the array, set current waypoint to 0
            if (num == waypoints.Length - 1){
                num = 0;
            }
            else{
                num = num + 1; // Increment value of current waypoint
            }
        }

        // Make transform move towards the position of the current waypoint at a set speed
        transform.position = Vector3.MoveTowards(transform.position, waypoints[num].transform.position, Time.fixedDeltaTime * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

}
