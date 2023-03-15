using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform cam;
    public Rigidbody rb;
    public float jumpHeight;
    public float speed;
    public float airMultiplier;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 respawnPos;
    public Vector3 moveDir;
    private bool grounded;
    public float groundDrag;
    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is on ground
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f,1);
        if (grounded){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }

        // Respawn avatar if falling into void below y = -50
        if (transform.position.y <= -50f){
            transform.position = respawnPos;
        }
            
        // Detect spacebar
        if (Input.GetButton("Jump") && canJump){
            // Performs groundcheck with raycast
            if (grounded){
                canJump = false;
                // reset y velocity
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                // Make avatar jump
                rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);

                Invoke("ResetJump",0.4f);
            }
        }
        

        // Detect wasd
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Make avatar move
        moveDir = transform.right * horizontalInput + transform.forward * verticalInput;

        // Control Max Speed
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > speed){
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    void FixedUpdate()
    {
        // Move Player
        if (grounded){
            rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Acceleration);
        }
        else if (!grounded){
            rb.AddForce(moveDir.normalized * speed * 10f * airMultiplier, ForceMode.Acceleration);
        }
        

        // Make avatar face in same direction as camera
        transform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(cam.position.x, 0, cam.position.z));
    }

    void ResetJump()
    {
        canJump = true;
    }

}
