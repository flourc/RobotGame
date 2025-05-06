using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    private float baseSpeed;
    public float jumpForce = 5f;
    public float jumpCooldown = 0.25f;
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
    private PlayerStatsCollector statsCollector;

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

    private float lastBaseSpeed = -1f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        statsCollector = PlayerStatsCollector.instance;


        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");

            transform.position = new Vector3(x, y, z);
            Debug.Log("Loaded player position from PlayerPrefs:");

            PlayerPrefs.DeleteKey("PlayerPosX");
            PlayerPrefs.DeleteKey("PlayerPosY");
            PlayerPrefs.DeleteKey("PlayerPosZ");
            PlayerPrefs.Save();
        }

        if (statsCollector != null)
        {
            SetBaseSpeed(statsCollector.GetCurrentMoveSpeed());
        }

        if (PlayerPositionSaver.hasSavedPosition)
        {
            transform.position = PlayerPositionSaver.savedPosition;
            PlayerPositionSaver.hasSavedPosition = false;
        }
    }

    private void Update()
    {
        if (statsCollector != null)
        {
            float currentSpeed = statsCollector.GetCurrentMoveSpeed();
            if (Mathf.Abs(currentSpeed - lastBaseSpeed) > 0.01f)
            {
                SetBaseSpeed(currentSpeed);
                
            }
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        MovePlayer();
        UpdateAnimationState();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

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
        }

        if (Input.GetMouseButton(1))
        {
            animator.Play("grab");
        }
        else
        {
            animator.SetBool("grabbing", false);
        }
    }

    private void MovePlayer()
    {
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? baseSpeed * 1.2f : baseSpeed;

        if (float.IsNaN(baseSpeed) || float.IsNaN(moveSpeed) || float.IsInfinity(moveSpeed))
        {
            Debug.LogError($"Invalid speed detected! baseSpeed: {baseSpeed}, moveSpeed: {moveSpeed}. Resetting to defaults.");
            baseSpeed = 7f;
            moveSpeed = 7f;
        }

        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection = inputDirection.normalized * moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);

        if (!grounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (grounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * gravity);
        }
    }

    public void SetBaseSpeed(float newSpeed)
    {
        if (float.IsNaN(newSpeed) || float.IsInfinity(newSpeed) || newSpeed <= 0f)
        {
            Debug.LogError($"Invalid base speed from stats collector: {newSpeed}. Using fallback.");
            baseSpeed = 7f;
            return;
        }

        baseSpeed = newSpeed;
        lastBaseSpeed = newSpeed;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
