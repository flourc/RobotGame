using UnityEngine;
using System.IO;

public class PlayerStatsCollector : MonoBehaviour
{
    public static PlayerStatsCollector instance;

    [Header("References")]
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    
    [Header("Shop UI Reference")]
    public ShopUI shopUI;
    
    [Header("Current Stats")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private int currentDamage;
    [SerializeField] private int currency = 0;

    private float savedPosX, savedPosY, savedPosZ;

    // Base stats for calculating upgrades
    private float baseMoveSpeed = 7f;
    private int baseDamage = 1;
    
    // Upgrade increments
    [SerializeField] private int speedUpgradeLevel = 0;
    [SerializeField] private int attackUpgradeLevel = 0;
    
    // Speed bonus per upgrade point (can be adjusted)
    [SerializeField] private float speedBonusPerLevel = 0.5f;
    
    // Cost per upgrade
    [SerializeField] private int upgradeCost = 10;

    // Save data path
    private string saveFilePath;

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
        
        // Set save file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "playerStats.json");
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
        
        if (shopUI == null)
        {
            shopUI = FindObjectOfType<ShopUI>();
        }
        
        // Load saved stats if available, otherwise use defaults
        LoadStats();
        
        // Store initial base values if not loaded from save
        if (playerMovement != null)
        {
            baseMoveSpeed = playerMovement.moveSpeed;
        }
        
        if (playerAttack != null)
        {
            baseDamage = playerAttack.damage;
        }
        
        // Apply current stats to player
        ApplyStats();
        
        // Initialize UI
        UpdateUI();
    }

    private void Update()
    {
        // Update stats every frame to catch any changes
        // UpdateAllStats();
    }
    
    // private void UpdateAllStats()
    // {
    //     if (playerMovement != null)
    //     {
    //         currentMoveSpeed = playerMovement.moveSpeed;
    //     }
        
    //     if (playerAttack != null)
    //     {
    //         currentDamage = playerAttack.damage;
    //     }
    // }

#if UNITY_EDITOR
    public static void ResetFromEditor()
    {
        PlayerStatsCollector temp = new PlayerStatsCollector(); // Or access existing instance
        temp.ResetSavedPlayerPosition();
    }
#endif
    
    private void UpdateUI()
    {
        // Update the single shop UI if it exists
        if (shopUI != null)
        {
            shopUI.UpdateUI();
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

    public void ResetSavedPlayerPosition()
    {
        SavePlayerPosition(Vector3.zero); // Or write your own logic to clear the file
        Debug.Log("Player position reset in JSON.");
    }

    
    public int GetAttackUpgradeLevel()
    {
        return attackUpgradeLevel;
    }
    
    public int GetSpeedUpgradeLevel()
    {
        return speedUpgradeLevel;
    }
    
    public int GetCurrency()
    {
        return currency;
    }
    
    public int GetUpgradeCost()
    {
        return upgradeCost;
    }
    
    // Methods to handle upgrades
    public void UpgradeSpeed(int points)
    {
        speedUpgradeLevel += points;
        
        // Apply the speed upgrade to the player
        float newSpeed = baseMoveSpeed + speedUpgradeLevel;
        SetMoveSpeed(newSpeed);
        
        // Update the UI
        if (shopUI != null)
        {
            shopUI.UpdateUI();
        }
        
        // Save stats after upgrade
        SaveStats();
    }
    
    public void UpgradeAttack(int points)
    {
        attackUpgradeLevel += points;
        
        // Apply the attack upgrade to the player
        SetDamage(baseDamage + attackUpgradeLevel);
        
        // Update the UI
        if (shopUI != null)
        {
            shopUI.UpdateUI();
        }
        
        // Save stats after upgrade
        SaveStats();
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
    
    // Currency methods
    public void AddCurrency(int amount)
    {
        currency += amount;
        if (Currency.instance != null)
        {
            Currency.instance.playerCurrency = currency;
            Currency.instance.CurrencyText.text = currency.ToString();
        }
        SaveStats();
    }
    
    public bool SpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            if (Currency.instance != null)
            {
                Currency.instance.playerCurrency = currency;
                Currency.instance.CurrencyText.text = currency.ToString();
            }

            SaveStats();
            return true;
        }
        return false;
    }
    
    // Try to purchase an upgrade
    public bool TryPurchaseUpgrade()
    {
        return SpendCurrency(upgradeCost);
    }
    
    // Serializable class for saving player stats
    [System.Serializable]
    private class PlayerStatsData
    {
        public float moveSpeed;
        public int damage;
        public int currency;
        public int speedUpgradeLevel;
        public int attackUpgradeLevel;

        public float positionX;
        public float positionY;
        public float positionZ;
    }
    
    // Save player stats to a file
    public void SaveStats()
    {
        PlayerStatsData data = new PlayerStatsData
        {
            moveSpeed = currentMoveSpeed,
            damage = currentDamage,
            currency = currency,
            speedUpgradeLevel = speedUpgradeLevel,
            attackUpgradeLevel = attackUpgradeLevel,
            positionX = savedPosX,
            positionY = savedPosY,
            positionZ = savedPosZ
        };
        
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
        
        Debug.Log("Player stats saved to: " + saveFilePath);
    }
    
    // Load player stats from file
    public void LoadStats()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerStatsData data = JsonUtility.FromJson<PlayerStatsData>(json);
            
            // Apply loaded data
            currentMoveSpeed = data.moveSpeed;
            currentDamage = data.damage;
            currency = data.currency;
            savedPosX = data.positionX;
            savedPosY = data.positionY;
            savedPosZ = data.positionZ;
            
            if (Currency.instance != null)
            {
                Currency.instance.playerCurrency = currency;
                Currency.instance.CurrencyText.text = currency.ToString();
            }
            

            speedUpgradeLevel = data.speedUpgradeLevel;
            attackUpgradeLevel = data.attackUpgradeLevel;
            
            Debug.Log("Player stats loaded from: " + saveFilePath);
        }
        else
        {
            Debug.Log("No saved player stats found. Using default values.");
        }
    }

    public void SetCurrency(int newCurrency){

        currency = newCurrency;
        SaveStats();

    }

    public void SavePlayerPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("PlayerPosX", position.x);
        PlayerPrefs.SetFloat("PlayerPosY", position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", position.z);
        PlayerPrefs.Save();
    }


    public Vector3 GetSavedPosition()
    {
        return new Vector3(savedPosX, savedPosY, savedPosZ);
    }
    
    // Apply current stats to player components
    private void ApplyStats()
    {
        SetMoveSpeed(currentMoveSpeed);
        SetDamage(currentDamage);
    }
    
    // Call this when the game is quitting or the scene is changing
    private void OnApplicationQuit()
    {

        SaveStats();
        ResetSavedPlayerPosition();
    }
}