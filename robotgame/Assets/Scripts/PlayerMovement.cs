using UnityEngine;

public class CombinedPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float groundDrag = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("References")]
    public Transform orientation;
    public Animator animator;
    public Rigidbody rb;

    // Movement and Input Variables
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool readyToJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        // Handle Input
        MyInput();

        // Speed Control and Drag
        SpeedControl();
        rb.drag = grounded ? groundDrag : 0;

        // Animation State
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        MovePlayer();
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
            // Walking animation based on movement input
            bool isWalking = horizontalInput != 0 || verticalInput != 0;
            animator.SetBool("walking", isWalking);

            // Ground state
            animator.SetBool("isGrounded", grounded);

            // Speed parameter
            animator.SetFloat("Speed", new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude);
        }
    }

    private void MovePlayer()
    {
        // Calculate move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Apply force based on ground state
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        // Limit velocity
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset vertical velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Apply jump force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}