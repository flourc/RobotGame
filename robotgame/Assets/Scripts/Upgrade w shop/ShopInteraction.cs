using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopInteraction : MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private string shopSceneName = "ShopScene";
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.Q;
    
    [Header("UI Elements")]
    // [SerializeField] private GameObject interactionPrompt;
    
    private Transform playerTransform;
    private bool playerInRange = false;
    
    private void Start()
    {
        // Find the player in the scene
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Hide the prompt initially
        // if (interactionPrompt != null)
        //     interactionPrompt.SetActive(false);
    }
    
    private void Update()
    {
        if (playerTransform == null) return;
        
        // Check if player is in range
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        bool isInRange = distance <= interactionDistance;
        
        // If player just entered range
        if (isInRange && !playerInRange)
        {
            playerInRange = true;
            // if (interactionPrompt != null)
            //     Debug.Log("In range");
            //     interactionPrompt.SetActive(true);
        }
        // If player just exited range
        else if (!isInRange && playerInRange)
        {
            playerInRange = false;
            // if (interactionPrompt != null)
            //     interactionPrompt.SetActive(false);
        }
        
        // Check for key press when in range
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            OpenShop();
        }
    }
    
    private void OpenShop()
    {
        Debug.Log("Opening shop...");
        
        // Save any necessary game state here before loading the shop
        
        // Load the shop scene
        SceneManager.LoadScene(shopSceneName);
    }
    
    // Optional: Visualize the interaction range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}