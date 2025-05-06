using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    [Header("Interaction Settings")]
    public KeyCode interactionKey = KeyCode.E;
    public float interactionDistance = 2f;
    public string shopPrompt = "Press E to open shop";
    
    [Header("UI References")]
    public GameObject interactionPrompt;
    public TMPro.TextMeshProUGUI promptText;
    
    private bool playerInRange = false;
    private Transform player;
    
    private void Start()
    {
        // Hide interaction prompt at start
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
        
        // Set the prompt text if provided
        if (promptText != null)
        {
            promptText.text = shopPrompt.Replace("E", interactionKey.ToString());
        }
        
        // Find the player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }
    
    private void Update()
    {
        CheckPlayerDistance();
        
        // Check for interaction input
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            OpenShop();
        }
    }
    
    private void CheckPlayerDistance()
    {
        if (player == null) return;
        
        // Calculate distance to player
        float distance = Vector3.Distance(transform.position, player.position);
        
        // Check if player is in range
        bool inRange = distance <= interactionDistance;
        
        // Update UI if needed
        if (inRange != playerInRange)
        {
            playerInRange = inRange;
            
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(playerInRange);
            }
        }
    }
    
    private void OpenShop()
    {
        
        
        // Find the shop manager
        ShopManager shopManager = ShopManager.instance;
        
        if (shopManager != null)
        {

            if (PlayerStatsCollector.instance != null && player != null){
                PlayerStatsCollector.instance.SavePlayerPosition(player.position);
            }
            PlayerPrefs.SetString("LastLevelName", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            
            Vector3 pos = player.position;

            PlayerPrefs.SetFloat("PlayerPosX", pos.x);
            PlayerPrefs.SetFloat("PlayerPosY", pos.y + 0.5f); // add Y offset to prevent sinking
            PlayerPrefs.SetFloat("PlayerPosZ", pos.z);
            PlayerPrefs.Save(); 

            Debug.Log("Saved position to PlayerPrefs: " + pos);
            


            shopManager.OpenShop();
        }
        else
        {
            Debug.LogError("ShopManager not found! Make sure it's in the scene.");
        }
    }
    
    // Optional: Draw gizmos in editor to see interaction range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}