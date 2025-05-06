using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Shop Elements")]
    [SerializeField] private Button attackUpgradeButton;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private TextMeshProUGUI currencyText;
    
    [Header("Attack Upgrade UI")]
    [SerializeField] private TextMeshProUGUI attackCostText;
    [SerializeField] private TextMeshProUGUI currentDamageText;
    [SerializeField] private TextMeshProUGUI attackLevelText;
    
    [Header("Speed Upgrade UI")]
    [SerializeField] private TextMeshProUGUI speedCostText;
    [SerializeField] private TextMeshProUGUI currentSpeedText;
    [SerializeField] private TextMeshProUGUI speedLevelText;
    
    [Header("Shop Navigation")]
    [SerializeField] private KeyCode exitShopKey = KeyCode.Escape;
    [SerializeField] private TextMeshProUGUI exitPromptText;
    [SerializeField] private string exitPrompt = "Press ESC to return to game";
    
    [Header("Notifications")]
    [SerializeField] private GameObject insufficientFundsNotification;
    [SerializeField] private float notificationDuration = 2f;
    
    [Header("Settings")]
    [SerializeField] private int upgradeCost = 10;
    
    private PlayerStatsCollector playerStats;
    
    private void Start()
    {
        // Get reference to PlayerStatsCollector
        playerStats = PlayerStatsCollector.instance;
        
        if (playerStats == null)
        {
            Debug.LogError("PlayerStatsCollector not found! Make sure it's in the scene.");
            return;
        }
        
        // Set up button listeners
        if (attackUpgradeButton != null)
        {
            // Make sure button is interactable
            attackUpgradeButton.interactable = true;
            attackUpgradeButton.onClick.AddListener(AttemptAttackUpgrade);
            Debug.Log("Attack button listener added");
        }
        
        if (speedUpgradeButton != null)
        {
            // Make sure button is interactable
            speedUpgradeButton.interactable = true;
            speedUpgradeButton.onClick.AddListener(AttemptSpeedUpgrade);
            Debug.Log("Speed button listener added");
        }
        
        // Set up exit prompt text
        if (exitPromptText != null)
        {
            exitPromptText.text = exitPrompt.Replace("ESC", exitShopKey.ToString());
        }

        // Unlock cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Initial UI update
        UpdateUI();
    }
    
    private void Update()
    {
        // Keep UI updated
        UpdateUI();
        
        // Check for exit key press
        if (Input.GetKeyDown(exitShopKey))
        {
            ExitShop();
        }
    }
    
    // Called to update all UI elements
    public void UpdateUI()
    {
        if (playerStats == null) return;
        
        int currentCurrency = Currency.instance != null ? Currency.instance.playerCurrency : 0;

        
        // Update currency display
        if (currencyText != null)
        {
            currencyText.text = "" + currentCurrency.ToString();
        }
        
        // Update attack upgrade UI
        if (attackCostText != null)
        {
            attackCostText.text = "Cost: " + upgradeCost.ToString();
        }
        
        if (currentDamageText != null)
        {
            currentDamageText.text = "Damage: " + playerStats.GetCurrentDamage().ToString();
        }
        
        if (attackLevelText != null)
        {
            attackLevelText.text = "Level: " + playerStats.GetAttackUpgradeLevel().ToString();
        }
        
        // Update speed upgrade UI
        if (speedCostText != null)
        {
            speedCostText.text = "Cost: " + upgradeCost.ToString();
        }
        
        if (currentSpeedText != null)
        {
            currentSpeedText.text = "Speed: " + playerStats.GetCurrentMoveSpeed().ToString("F1");
        }
        
        if (speedLevelText != null)
        {
            speedLevelText.text = "Level: " + playerStats.GetSpeedUpgradeLevel().ToString();
        }
        
        // Enable/disable buttons based on available currency
        bool canAfford = currentCurrency >= upgradeCost;
        
        if (attackUpgradeButton != null)
        {
            attackUpgradeButton.interactable = canAfford;
        }
        
        if (speedUpgradeButton != null)
        {
            speedUpgradeButton.interactable = canAfford;
        }
    }
    
    // Called when the attack upgrade button is clicked
    public void AttemptAttackUpgrade()
    {
        if (playerStats == null) return;
        
        if (playerStats.TryPurchaseUpgrade())
        {
            // Successfully purchased upgrade
            playerStats.UpgradeAttack(1);
            Debug.Log("Attack upgraded! New damage: " + playerStats.GetCurrentDamage());
        }
        else
        {
            // Not enough currency
            Debug.Log("Not enough currency to upgrade attack!");
            ShowInsufficientFundsNotification();
        }
    }
    
    // Called when the speed upgrade button is clicked
    public void AttemptSpeedUpgrade()
    {
        if (playerStats == null) return;
        
        if (playerStats.TryPurchaseUpgrade())
        {
            // Successfully purchased upgrade
            playerStats.UpgradeSpeed(1);
            Debug.Log("Speed upgraded! New speed: " + playerStats.GetCurrentMoveSpeed());
        }
        else
        {
            // Not enough currency
            Debug.Log("Not enough currency to upgrade speed!");
            ShowInsufficientFundsNotification();
        }
    }
    
    // Show notification for insufficient funds
    private void ShowInsufficientFundsNotification()
    {
        if (insufficientFundsNotification != null)
        {
            StartCoroutine(ShowNotification());
        }
    }


    public void ExitShop()
    {
        string lastLevelName = PlayerPrefs.GetString("LastLevelName", "Level_1"); // Default fallback
        
        if (!string.IsNullOrEmpty(lastLevelName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(lastLevelName);
            Debug.Log($"Returning to previous level: {lastLevelName}");
        }
        else
        {
            Debug.LogWarning("LastLevelName not found in PlayerPrefs. Loading default scene.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
        }
    }

    
    private System.Collections.IEnumerator ShowNotification()
    {
        insufficientFundsNotification.SetActive(true);
        yield return new WaitForSeconds(notificationDuration);
        insufficientFundsNotification.SetActive(false);
    }
}