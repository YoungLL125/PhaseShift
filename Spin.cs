using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.up * Time.fixedDeltaTime * speed, Space.Self);
    }
}
