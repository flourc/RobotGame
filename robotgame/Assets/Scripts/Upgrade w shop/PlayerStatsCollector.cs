using UnityEngine;

public class PlayerStatsCollector : MonoBehaviour
{
    public static PlayerStatsCollector instance;

    [Header("References")]
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    
    [Header("Shop UI References")]
    public AttackShopUI attackShopUI;
    public SpeedShopUI speedShopUI;
    
    [Header("Current Stats")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private int currentDamage;
    
    // Base stats for calculating upgrades
    private float baseMoveSpeed = 5f;
    private int baseDamage = 1;
    
    // Upgrade increments
    [SerializeField] private int speedUpgradeLevel = 0;
    [SerializeField] private int attackUpgradeLevel = 0;
    
    // Speed bonus per upgrade point (can be adjusted)
    [SerializeField] private float speedBonusPerLevel = 0.5f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // If references aren't set in the inspector, try to find them
        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }
        
        if (playerAttack == null)
        {
            playerAttack = FindObjectOfType<PlayerAttack>();
        }
        
        if (attackShopUI == null)
        {
            attackShopUI = FindObjectOfType<AttackShopUI>();
        }
        
        if (speedShopUI == null)
        {
            speedShopUI = FindObjectOfType<SpeedShopUI>();
        }
        
        // Store initial base values
        if (playerMovement != null)
        {
            baseMoveSpeed = playerMovement.moveSpeed;
        }
        
        if (playerAttack != null)
        {
            baseDamage = playerAttack.damage;
        }
        
        // Initialize stats
        UpdateAllStats();
        
        // Initialize UI
        UpdateUI();
    }

    private void Update()
    {
        // Update stats every frame to catch any changes
        UpdateAllStats();
    }
    
    private void UpdateAllStats()
    {
        if (playerMovement != null)
        {
            currentMoveSpeed = playerMovement.moveSpeed;
            
        }
        
        if (playerAttack != null)
        {
            currentDamage = playerAttack.damage;
            
        }
    }
    
    private void UpdateUI()
    {
        if (attackShopUI != null)
        {
            // Update the attack UI with current upgrade level
            attackShopUI.upgradeAttack(0); // This refreshes the display without adding points
        }
        
        if (speedShopUI != null)
        {
            // Update the speed UI with current upgrade level
            speedShopUI.UpgradeSpeed(0); // This refreshes the display without adding points
        }
    }
    
    // Getter methods for other scripts to access the stats
    public float GetCurrentMoveSpeed()
    {
        return currentMoveSpeed;
    }
    
    public int GetCurrentDamage()
    {
        return currentDamage;
    }
    
    public int GetAttackUpgradeLevel()
    {
        return attackUpgradeLevel;
    }
      public int GetSpeedUpgradeLevel()
    {
        return speedUpgradeLevel;
    }
    
    // Methods to handle upgrades
    public void UpgradeSpeed(int points)
    {
        speedUpgradeLevel += points;
        
        // Apply the speed upgrade to the player
        float newSpeed = baseMoveSpeed + (speedUpgradeLevel * speedBonusPerLevel);
        SetMoveSpeed(newSpeed);
        
        // Update the UI
        if (speedShopUI != null)
        {
            speedShopUI.UpgradeSpeed(points);
        }
    }
    
    public void UpgradeAttack(int points)
    {
        attackUpgradeLevel += points;
        
        // Apply the attack upgrade to the player
        SetDamage(baseDamage + attackUpgradeLevel);
        
        // Update the UI
        if (attackShopUI != null)
        {
            attackShopUI.upgradeAttack(points);
        }
    }
    
    // Setter methods if you want to modify these stats from elsewhere
    public void SetMoveSpeed(float newSpeed)
    {
        if (playerMovement != null)
        {
            playerMovement.moveSpeed = newSpeed;
            currentMoveSpeed = newSpeed;
        }
    }
    
    public void SetDamage(int newDamage)
    {
        if (playerAttack != null)
        {
            playerAttack.damage = newDamage;
            currentDamage = newDamage;
        }
    }
    
    
    
   
}