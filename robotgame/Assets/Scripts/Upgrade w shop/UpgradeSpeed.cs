using UnityEngine;
using UnityEngine.UI;

public class UpgradeSpeed : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    
    private PlayerStatsCollector playerStats;
    
    void Start()
    {
        // Get reference to the PlayerStatsCollector
        playerStats = PlayerStatsCollector.instance;
        
        if (playerStats == null)
        {
            Debug.LogError("PlayerStatsCollector not found! Make sure it's in the scene.");
            enabled = false;
            return;
        }
        
        // Set up button click listener
        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(AttemptUpgrade);
        }
        else
        {
            Debug.LogWarning("Upgrade button reference not set!");
        }
    }
    
    public void AttemptUpgrade()
    {
        if (playerStats == null) return;
        
        // Check if player has enough currency
        if (playerStats.TryPurchaseUpgrade())
        {
            // Successful purchase, upgrade speed
            playerStats.UpgradeSpeed(1);
            Debug.Log("Speed upgraded! New speed: " + playerStats.GetCurrentMoveSpeed());
        }
        else
        {
            // Not enough currency
            Debug.Log("Not enough currency to upgrade speed!");
        }
    }
}