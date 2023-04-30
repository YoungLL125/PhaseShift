using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float sensX;
    public float sensY;
    float xRotation;
    float yRotation;
    public Transform cam;
    public Rigidbody rb;
    public Material checkpointed;
    public Material uncheckpointed;
    public float jumpForce;
    public float speed;
    public float walkSpeed = 10f;
    public float sprintSpeed = 20f;
    public float airMultiplier;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 respawnPos;
    public Vector3 moveDir;
    private bool grounded;
    public float groundDrag;
    private bool canJump = true;
    public Vector3 camOffset;
    public float checkpoint = 0;
    public GameObject[] checkpointobjs;

    // Start is called before the first frame update
    void Start()
    {
        // Sets cursor locked in centre of screen and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

        rb = GetComponent<Rigidbody>(); // Gets rigidbody component

        checkpointobjs = GameObject.FindGameObjectsWithTag("Checkpoint"); // Gets all checkpoint gameobjs
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
        grounded = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 0.6f, 1);
        if (grounded){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0.1f;
        }

        // Respawn avatar if falling into void below y = -100
        if (transform.position.y <= -100f){
            transform.position = respawnPos;
        }
            
        // Detect spacebar, Performs groundcheck with raycast, checks for jump delay
        if (Input.GetButton("Jump") && grounded && canJump){
            // jump delay bool
            canJump = false;
            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            // Make avatar jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            Invoke("ResetJump",0.4f); // Run reset jump function in 0.4 seconds
        }

        // Detect sprint (R key)
        if (Input.GetButtonDown("Shift")){
            speed = sprintSpeed; // Set max speed to sprint speed
        }

        // Detect wasd
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput + verticalInput == 0){
            speed = walkSpeed;
        }

        // Make avatar move
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
        // Move Player
        if (grounded){
            rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Acceleration);
        }
        else if (!grounded){
            rb.AddForce(moveDir.normalized * speed * 10f * airMultiplier, ForceMode.Acceleration);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint" && checkpoint != other.name[10]){
            // Resets colour of all checkpoints
            foreach(GameObject obj in checkpointobjs){
                obj.GetComponent<Renderer>().material = uncheckpointed;
            }
            checkpoint = other.name[10]; // The number of the checkpoint
            other.GetComponent<Renderer>().material = checkpointed; // Changes the colour of the checkpoint
            respawnPos = other.transform.position + new Vector3(0,1.5f,0); // Sets spawnpoint
        }
    }

}
