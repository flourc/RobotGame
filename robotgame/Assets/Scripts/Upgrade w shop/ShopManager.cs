using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    // [SerializeField] private string mainGameSceneName = "GameScene";
    [SerializeField] private string shopSceneName = "ShopScene";
    
    // Player position tracking
    private Vector3 playerPositionBeforeShop;
    private string currentLevelName;
    
    // Reference to notification panel
    [SerializeField] private GameObject insufficientFundsNotification;
    [SerializeField] private float notificationDuration = 2f;
    
    // Static instance for easy access from other scripts
    public static ShopManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Called to open the shop UI
    public void OpenShop()
    {
        // Store the current scene name
        currentLevelName = SceneManager.GetActiveScene().name;
        Debug.Log(currentLevelName);
        // Store the player's position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPositionBeforeShop = player.transform.position;
            Debug.Log("Saved player position: " + playerPositionBeforeShop);
            
            // Disable any player controller components that could interfere with UI
            DisablePlayerControls(player);
        }
        
        // Load the shop scene
        SceneManager.LoadScene(shopSceneName);
    }
    
    // Disable player controls that might interfere with shop UI
    private void DisablePlayerControls(GameObject player)
    {
        // Common controller types - disable what you're using in your game
        MonoBehaviour[] controllers = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour controller in controllers)
        {
            // Check for common controller types
            string controllerName = controller.GetType().Name.ToLower();
            if (controllerName.Contains("controller") || 
                controllerName.Contains("movement") || 
                controllerName.Contains("motor") ||
                controllerName.Contains("input") ||
                controllerName.Contains("character"))
            {
                // Save enabled state before disabling
                Debug.Log("Disabling player controller: " + controller.GetType().Name);
                controller.enabled = false;
            }
        }
        
        // If using cursor lock in your game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    // Called when exiting the shop
    public void CloseShop()
    {
        // Return to the saved level the player was in
        string sceneToLoad = string.IsNullOrEmpty(currentLevelName) 
            ? SceneManager.GetActiveScene().name 
            : currentLevelName;

        SceneManager.LoadScene(sceneToLoad);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    // Called after the scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only process if returning to the gameplay scene
        if (scene.name == currentLevelName)
        {
            // Find the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player != null)
            {
                // Restore the player's position
                if (playerPositionBeforeShop != Vector3.zero)
                {
                    player.transform.position = playerPositionBeforeShop;
                    Debug.Log("Restored player to position: " + playerPositionBeforeShop);
                }
                
                // Re-enable player controls
                EnablePlayerControls(player);
            }
        }
        
        // Unregister the callback to avoid multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // Re-enable player controls that were disabled when opening shop
    private void EnablePlayerControls(GameObject player)
    {
        // Re-enable all movement/controller components
        MonoBehaviour[] controllers = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour controller in controllers)
        {
            string controllerName = controller.GetType().Name.ToLower();
            if (controllerName.Contains("controller") || 
                controllerName.Contains("movement") || 
                controllerName.Contains("motor") ||
                controllerName.Contains("input") ||
                controllerName.Contains("character"))
            {
                Debug.Log("Re-enabling player controller: " + controller.GetType().Name);
                controller.enabled = true;
            }
        }
        
        // If your game uses locked cursor, re-lock it here
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
    
    // Can be called to display the insufficient funds notification
    public void ShowInsufficientFundsNotification()
    {
        if (insufficientFundsNotification != null)
        {
            StartCoroutine(ShowNotification());
        }
    }
    
    private System.Collections.IEnumerator ShowNotification()
    {
        insufficientFundsNotification.SetActive(true);
        yield return new WaitForSeconds(notificationDuration);
        insufficientFundsNotification.SetActive(false);
    }
}