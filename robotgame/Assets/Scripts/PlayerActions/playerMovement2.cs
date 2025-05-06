using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement2 : MonoBehaviour
{
	private Animator animator;
    [Header("Movement")]
    public float moveSpeed = 10f;

    public float groundDrag;

    public float jumpForce = 18f;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded = true;

    [Header("Audio")]
    public AudioSource walkAudio;
    public AudioSource runAudio;  

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
		animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, 1f, whatIsGround);

        Debug.DrawRay(transform.position, Vector3.down * 1f, Color.red); 

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        // Animation State
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

		if ((grounded) && ((horizontalInput) != 0 || (verticalInput != 0))){
			animator.SetBool("Walking", true);
		} else {
			animator.SetBool("Walking", false);
		}

    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void UpdateAnimationState()
    {
        if (animator != null)
        {
            // Check if player is moving
            bool isMoving = horizontalInput != 0 || verticalInput != 0;
            
            // Set speed parameter based on movement
            float currentSpeed = 0f; // Default to idle
            
            if (isMoving && grounded)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    currentSpeed = 1f; // Running
                    moveSpeed = 7f * 1.2f;
                    animator.SetBool("Running", true);
                    animator.SetBool("Walking", false);

                    if (!runAudio.isPlaying)
                    {
                        //Debug.Log("playing audio!");
                        runAudio.Play();
                    }
                    if (walkAudio.isPlaying)
                    {
                        walkAudio.Stop();
                    }
                    
                }
                else
                {
                    currentSpeed = 0.5f; // Walking
                    moveSpeed = 7f;
                    animator.SetBool("Walking", true);
                    animator.SetBool("Running", false);

                    if (!walkAudio.isPlaying)
                    {
                        //Debug.Log("playing audio!");
                        walkAudio.Play();
                    }
                    if (runAudio.isPlaying)
                    {
                        runAudio.Stop();
                    }


                }
            }
            else
            {
                // Not moving - explicitly reset all movement booleans
                animator.SetBool("Walking", false);
                animator.SetBool("Running", false);
                if (walkAudio.isPlaying) {walkAudio.Stop();}
                if (runAudio.isPlaying) {runAudio.Stop();}

            }
            
            // Set the speed parameter that controls the blend tree
            // animator.SetFloat("Speed", currentSpeed);
            //Debug.Log("Speed Parameter: " + currentSpeed); // Debug output
            
            // Rest of your code for combat animations...
            print(rb.velocity.y);
            //[JUMP STUFF]
 
            if (!grounded && rb.velocity.y > 0) {
                print("jumping");
                animator.SetBool("jumping", true);
            }
            else if (!grounded && rb.velocity.y <= 0) {
                animator.SetBool("landing", true);
            }

            if (grounded || rb.velocity.y == 0) {
                animator.SetBool("landing", false);
                animator.SetBool("jumping", false);
            }
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

	// void OnCollisionEnter(Collision other){
	// 	if (other.gameObject.layer==LayerMask.NameToLayer("whatIsGround")){
	// 		grounded = true;
	// 		//Debug.Log("I am touching floor");
	// 	}
	// }

	// void OnCollisionExit(Collision other){
	// 	if (other.gameObject.layer==LayerMask.NameToLayer("whatIsGround")){
	// 		grounded = false;
	// 		//Debug.Log("I am not touching floor floor");
	// 	}
	// }

}