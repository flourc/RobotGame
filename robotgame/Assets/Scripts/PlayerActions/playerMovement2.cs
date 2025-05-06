using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Animator animator;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public float baseSpeed;

    public float groundDrag;
    public float jumpForce = 18f;
    public float fallMultiplier = 9f;
    public float jumpCooldown = 0.2f;
    public float gravity = 18f;
    public float airMultiplier = 0.6f;

    private bool readyToJump = true;
    private float lastBaseSpeed = -1f;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Audio")]
    public AudioSource walkAudio;
    public AudioSource runAudio;

    [Header("References")]
    public Transform orientation;

    private Rigidbody rb;
    private PlayerStatsCollector statsCollector;
    
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    Vector3 velocity;


    
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        statsCollector = PlayerStatsCollector.instance;

        if (statsCollector != null)
        {
            SetBaseSpeed(statsCollector.GetCurrentMoveSpeed());
        }
        else
        {
            Debug.LogWarning("PlayerStatsCollector not found! Using default speed values.");
        }

        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            transform.position = new Vector3(x, y, z);

            PlayerPrefs.DeleteKey("PlayerPosX");
            PlayerPrefs.DeleteKey("PlayerPosY");
            PlayerPrefs.DeleteKey("PlayerPosZ");
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;

        // Dynamically pull speed from stats collector if changed
        if (statsCollector != null)
        {
            float currentSpeed = statsCollector.GetCurrentMoveSpeed();
            Debug.Log($"Stats collector returned speed: {currentSpeed}, lastBaseSpeed: {lastBaseSpeed}");
            
            if (Mathf.Abs(currentSpeed - lastBaseSpeed) > 0.01f)
            {
                SetBaseSpeed(currentSpeed);
            }
        }
        else
        {
            // If we somehow lost the reference to the stats collector, try to find it again
            statsCollector = PlayerStatsCollector.instance;
            if (statsCollector != null)
            {
                Debug.Log("Reconnected to PlayerStatsCollector");
            }
        }

        MyInput();
        UpdateAnimationState();
        SpeedControl(); // Re-enabled speed control to prevent excessive velocity
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? baseSpeed * 1.5f : baseSpeed;
        moveSpeed = Mathf.Clamp(moveSpeed, 1f, 20f); // Clamp to avoid going infinite

        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection = inputDirection.normalized;

        if (grounded)
        {
            rb.drag = groundDrag;
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.drag = 0f;
            rb.AddForce(moveDirection * moveSpeed * airMultiplier, ForceMode.Force);
            velocity.y -= gravity * Time.deltaTime;
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void SetBaseSpeed(float newSpeed)
    {
        if (float.IsNaN(newSpeed) || float.IsInfinity(newSpeed) || newSpeed <= 0f)
        {
            Debug.LogError($"Invalid base speed from stats collector: {newSpeed}. Using fallback of 5f.");
            baseSpeed = 5f; // Set a fallback value
        }
        else
        {
            baseSpeed = newSpeed; // This line was missing in the original
            Debug.Log($"Updated player base speed to: {baseSpeed}");
        }
        
        // Always update lastBaseSpeed to prevent continuous attempts to update
        lastBaseSpeed = newSpeed;
    }

    private void UpdateAnimationState()
    {
        if (animator == null) return;

        bool isMoving = horizontalInput != 0 || verticalInput != 0;

        if (isMoving && grounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("Running", true);
                animator.SetBool("Walking", false);
                if (!runAudio.isPlaying) runAudio.Play();
                if (walkAudio.isPlaying) walkAudio.Stop();
            }
            else
            {
                animator.SetBool("Walking", true);
                animator.SetBool("Running", false);
                if (!walkAudio.isPlaying) walkAudio.Play();
                if (runAudio.isPlaying) runAudio.Stop();
            }
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            if (walkAudio.isPlaying) walkAudio.Stop();
            if (runAudio.isPlaying) runAudio.Stop();
        }

        // Jumping/falling
        animator.SetBool("jumping", !grounded && rb.velocity.y > 0);
        animator.SetBool("landing", !grounded && rb.velocity.y <= 0);
        if (grounded) {
            animator.SetBool("jumping", false);
            animator.SetBool("landing", false);
        }
    }
}