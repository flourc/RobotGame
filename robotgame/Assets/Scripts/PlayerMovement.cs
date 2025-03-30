using UnityEngine;

public class CombinedPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 0.25f;
    public float gravity = 9.81f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("References")]
    public Transform orientation;
    public Animator animator;
    public CharacterController characterController;

    // Movement and Input Variables
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Vector3 velocity;
    bool readyToJump = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        if (animator == null)
            animator = GetComponent<Animator>();
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
    }

private void UpdateAnimationState()
{
    if (animator != null)
    {
        print("hi");
        // Movement animations
        bool isMoving = horizontalInput != 0 || verticalInput != 0;
        animator.SetBool("walking", isMoving && !Input.GetKey(KeyCode.LeftShift));
        
        // // Running animation (Shift + WASD)
        // animator.SetBool("run", isMoving && Input.GetKey(KeyCode.LeftShift));
        
        // Jump/ground state
        animator.SetBool("isGrounded", grounded);
        
        // Speed parameter for blend trees if needed
        animator.SetFloat("Speed", new Vector3(moveDirection.x, 0, moveDirection.z).magnitude);
        
        // Combat animations
        if (Input.GetMouseButtonDown(1)) // Right-click for stab/slash
        {
            animator.SetTrigger("slash");
        }
        
        if (Input.GetMouseButtonDown(0)) // Left-click for grab
        {
            animator.SetTrigger("grab");
        }
        
        // Reset triggers to prevent animation issues
        if (Input.GetMouseButtonUp(0))
        {
            animator.ResetTrigger("grab");
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            animator.ResetTrigger("slash");
        }
    }
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
            velocity.y -= gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep character grounded
        }

        // Apply vertical velocity
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (grounded)
        {
            // Apply jump velocity
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}