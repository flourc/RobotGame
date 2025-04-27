using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public CharacterController characterController; // Changed from Rigidbody to CharacterController
    public float rotationSpeed;
    public GameObject thirdPersonCam;
    
    [Header("Cursor Settings")]
    [SerializeField] private KeyCode cursorToggleKey = KeyCode.Escape; // Key to toggle cursor
    private bool isCursorLocked = true;

    public bool snap;
    public PlayerAttack pa;
    
    private void Start()
    {
        // If the character controller wasn't set in the inspector, try to find it
        if (characterController == null)
        {
            characterController = player.GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("No CharacterController found on the player. Please assign it in the inspector.");
            }
        }
        
        // Lock cursor at start
        LockCursor();
    }
    
    private void Update()
    {
        // Handle cursor locking
        if (Input.GetKeyDown(cursorToggleKey))
        {

            ToggleCursorLock();
        }
        
        // Rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        
        if (pa.returnSnap()) {
            // viewDir = player.position;
        }
        else {
            orientation.forward = viewDir.normalized;
        
        // Rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        // orientation.forward = viewDir.normalized;
        
        // // Rotate player object
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");
        // Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        // if (inputDir != Vector3.zero)
        //     playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }
    
    // Cursor Control Methods
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;

        //temp;
    }
    
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }
    
    public void ToggleCursorLock()
    {
        if (isCursorLocked)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }
    
    // Optional: Public method to allow other scripts to check cursor state
    public bool IsCursorLocked()
    {
        return isCursorLocked;
    }

    
}
