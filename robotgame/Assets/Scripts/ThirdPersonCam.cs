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
    public Transform combatLookAt;
    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject topDownCam;
    public CameraStyle currentStyle;
    
    [Header("Cursor Settings")]
    [SerializeField] private KeyCode cursorToggleKey = KeyCode.Escape; // Key to toggle cursor
    private bool isCursorLocked = true;
    
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }
    
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
        
        // Switch camera styles
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.Topdown);
        
        // Rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        
        // Rotate player object
        if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            
            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;
            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }
    
    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);
        
        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true);
        
        currentStyle = newStyle;
    }
    
    // Cursor Control Methods
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
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