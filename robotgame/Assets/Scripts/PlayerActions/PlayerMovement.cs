using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    private float baseSpeed;
    public float jumpForce = 5f;
    public float jumpCooldown = 0.25f;
    // public float gravity = 9.81f;
     public float gravity = 18f;

    public float fallMultiplier = 9f;
    public float lowJumpMultiplier = 8.5f;


    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("References")]
    public Transform orientation;
    public Animator animator;
    public CharacterController characterController;

    [Header("Audio")]
    public AudioSource walkAudio;
    public AudioSource runAudio;    

    // Movement and Input Variables
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Vector3 velocity;
    public bool readyToJump = true;

    public GameObject gunArm;
    public GameObject swordArm;
    // public AudioSource audios;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (PlayerStatsCollector.instance != null){

            baseSpeed = PlayerStatsCollector.instance.GetCurrentMoveSpeed();
            moveSpeed = baseSpeed;

        }
        // audios = GetComponent<AudioSource>();
        // if (animator == null)
        //     animator = GetComponent<Animator>();
    }

    private void Update()
    {

        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        // Handle Input
        MyInput();

        // Movement
        MovePlayer();

        // Animation State
        UpdateAnimationState();
    }


    private void MyInput()
    {
        // Get input axes
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump Logic
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);

        }

        // if (animator.GetBool("Walking") == true && !audios.isPlaying) {
        //     audios.Play();
        // }
        // else if (animator.GetBool("Walking") == false && audios.isPlaying){
        //     audios.Stop();
        // }
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
                moveSpeed = baseSpeed * 1.2f;
                animator.SetBool("Running", true);
                animator.SetBool("Walking", false);

                if (!runAudio.isPlaying)
                {
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
                moveSpeed = baseSpeed;
                animator.SetBool("Walking", true);
                animator.SetBool("Running", false);

                if (!walkAudio.isPlaying)
                {
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
    }
        
        // // Combat animations - Change to use SetBool instead of SetTrigger
        // if (Input.GetMouseButtonDown(0)) // Left-click hold for grab
        // {
        //     if (swordArm.activeSelf) {
        //         animator.Play("slash");
        //     } 
        //     else if (gunArm.activeSelf) {
        //         animator.Play("shoot");
        //     }
        // }

        

        if (Input.GetMouseButton(1)) // Right-click hold for slash
        {
            // animator.SetBool("grabbing", true);
            animator.Play("grab");
        }
        else
        {
            animator.SetBool("grabbing", false);
        }

        //Debug.Log("Current State: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
        //Debug.Log("Is Walking: " + animator.GetBool("Walking"));
        //Debug.Log("Is Running: " + animator.GetBool("Running"));

}


    private void MovePlayer()
    {
        // Calculate move direction
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection = inputDirection.normalized * moveSpeed;

        // Apply movement
        characterController.Move(moveDirection * Time.deltaTime);

        // Handle gravity
        if (!grounded)
        {
            velocity.y -= gravity * Time.deltaTime; //TEMP
            
        }
        else if (!grounded && velocity.y < 0)
        {
           //velocity.y = -3f; // Small downward force to keep character grounded
           velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; //test
        } //TEMP

        // Apply vertical velocity
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (grounded)
        {
            // Apply jump velocity
            velocity.y = Mathf.Sqrt(jumpForce * gravity);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}