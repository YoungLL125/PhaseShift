using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;
    float xRotation;
    float yRotation;
    public Transform cam;
    public Rigidbody rb;
    public Material checkpointed;
    public Material uncheckpointed;
    public float jumpForce = 20f;
    public float speed = 20f;
    public float walkSpeed = 10f;
    public float sprintSpeed = 20f;
    public float airMultiplier;
    public float groundDrag = 10f;
    public float airDrag = 0.1f;
    public float horizontalInput;
    public float verticalInput;
    public bool alive = true;
    public Vector3 respawnPos;
    public Vector3 moveDir;
    private bool grounded;
    private bool canJump = true;
    public Vector3 camOffset;
    public string checkpoint = "0";
    public GameObject[] checkpointobjs;

    // Start is called before the first frame update
    void Start()
    {
        // Sets cursor locked in centre of screen and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

        rb = GetComponent<Rigidbody>(); // Gets rigidbody component

        checkpointobjs = GameObject.FindGameObjectsWithTag("Checkpoint"); // Gets all checkpoint gameobjs

        checkpoint = "0"; // Initiates current checkpoint
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse Inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Link Mouse location to the character rotation
        yRotation = yRotation + mouseX;
        xRotation = xRotation - mouseY;

        xRotation = Mathf.Clamp(xRotation,-90f,90f); // Prevents camera rotating past 90 degrees

        // Rotate cam and player
        cam.rotation = Quaternion.Euler(xRotation,yRotation,0);
        transform.rotation = Quaternion.Euler(0,yRotation,0);
        
        // Check if player is on ground
        RaycastHit hit;
        grounded = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 0.501f, 1);
        if (grounded){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = airDrag; // Air drag
        }

        // Respawn player if falling into void below y = -100
        if (transform.position.y <= -100f){
            Die();
        }
            
        // Detect spacebar, Performs groundcheck with raycast, checks for jump delay
        if (Input.GetButton("Jump") && grounded && canJump){
            // jump delay bool
            canJump = false;
            rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
            // Make avatar jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

            Invoke("ResetJump",0.1f); // Run reset jump function in 0.4 seconds
        }

        // Increase max speed if sprint key (shift) is held down
        if (Input.GetButton("Shift")){
            speed = sprintSpeed; // Set max speed to sprint speed
        }

        // Detect wasd
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Reset Sprinting if player isn't moving
        if (horizontalInput + verticalInput == 0){
            speed = walkSpeed;
        }

        // Set move direction
        moveDir = transform.right * horizontalInput + transform.forward * verticalInput;

        // Get flat velocity
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        // Control Max Speed
        if (flatVel.magnitude > speed && grounded){
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    void FixedUpdate()
    {
        if (alive == true){
            // Move Player
            if (grounded){
                rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Acceleration);
            }
            else if (!grounded){
                rb.AddForce(moveDir.normalized * speed * 10f * airMultiplier, ForceMode.Acceleration);
            }
        }
    }

    void LateUpdate()
    {
        cam.position = transform.position + camOffset; // Sets camera position to player position
    }

    void ResetJump()
    {
        canJump = true; // Reset Jump
    }

    // Collision detection
    void OnTriggerEnter(Collider other)
    {
        // Checkpoint manager
        if (other.tag == "Checkpoint" && checkpoint != other.name.Remove(0,10)){
            // Resets colour of all checkpoints
            foreach(GameObject obj in checkpointobjs){
                obj.GetComponent<Renderer>().material = uncheckpointed;
            }
            checkpoint = other.name.Remove(0,10); // The number of the checkpoint
            other.GetComponent<Renderer>().material = checkpointed; // Changes the colour of the checkpoint
            respawnPos = other.transform.position + new Vector3(0,1.5f,0); // Sets spawnpoint
        }

        // Kill Player when colliding with spikes
        if (other.tag == "Spike"){
            Die();
        }
    }

    void Die()
    {
        alive = false;
        transform.position = respawnPos;
        alive = true;
    }

}
