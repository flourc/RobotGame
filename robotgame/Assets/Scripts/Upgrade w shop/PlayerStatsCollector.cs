using UnityEngine;
using System.IO;
using System.Collections;

public class PlayerStatsCollector : MonoBehaviour
{
    public static PlayerStatsCollector instance;

    [Header("References")]
    public PlayerMovement2 playerMovement2;
    public PlayerAttack playerAttack;

    [Header("Shop UI Reference")]
    public ShopUI shopUI;

    [Header("Current Stats")]
    [SerializeField] private float currentMoveSpeed = 10f; // Initialize to a default value
    [SerializeField] private int currentDamage;
    [SerializeField] private int currency = 0;

    [Header("Base Stats")]
    [SerializeField] private float baseMoveSpeed = 10f;
    [SerializeField] private int baseDamage = 1;

    [Header("Upgrades")]
    [SerializeField] private int speedUpgradeLevel = 0;
    [SerializeField] private int attackUpgradeLevel = 0;
    [SerializeField] private float speedBonusPerLevel = 0.5f;
    [SerializeField] private int upgradeCost = 10;

    private string saveFilePath;
    private bool hasInitialized = false;

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
            return;
        }

        saveFilePath = Path.Combine(Application.persistentDataPath, "playerStats.json");
        
        // Initialize currentMoveSpeed to match baseMoveSpeed
        currentMoveSpeed = baseMoveSpeed;
        
        Debug.Log($"[PlayerStatsCollector] Awake - Initial currentMoveSpeed: {currentMoveSpeed}");
    }
    
    private IEnumerator Start()
    {
        Debug.Log($"[PlayerStatsCollector] Start coroutine beginning");
        
        yield return new WaitUntil(() => Currency.instance != null);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (playerMovement2 == null)
                playerMovement2 = player.GetComponent<PlayerMovement2>();
            if (playerAttack == null)
                playerAttack = player.GetComponent<PlayerAttack>();
        }

        if (shopUI == null)
            shopUI = FindObjectOfType<ShopUI>();

        bool hadSave = LoadStats();
        
        // Only get base stats from player components if no save was found
        if (!hadSave)
        {
            // Only use player values as fallback, don't overwrite loaded values
            Debug.Log($"[PlayerStatsCollector] Using fallback values from player components");
            if (playerMovement2 != null && playerMovement2.baseSpeed > 0)
                baseMoveSpeed = playerMovement2.baseSpeed;
                
            if (playerAttack != null && playerAttack.damage > 0)
                baseDamage = playerAttack.damage;
        }

        ApplyStats();
        UpdateUI();
        
        hasInitialized = true;
        Debug.Log($"[PlayerStatsCollector] Initialization complete - currentMoveSpeed: {currentMoveSpeed}");
    }

    private void UpdateUI() => shopUI?.UpdateUI();

    public float GetCurrentMoveSpeed() 
    {
        Debug.Log($"[PlayerStatsCollector] GetCurrentMoveSpeed called, returning: {currentMoveSpeed}");
        return currentMoveSpeed;
    }
    
    public int GetCurrentDamage() => currentDamage;
    public int GetAttackUpgradeLevel() => attackUpgradeLevel;
    public int GetSpeedUpgradeLevel() => speedUpgradeLevel;
    public int GetCurrency() => currency;
    public int GetUpgradeCost() => upgradeCost;

    public void SetCurrency(int newCurrency)
    {
        currency = newCurrency;
        SaveStats();
    }

    public void UpgradeSpeed(int points)
    {
        speedUpgradeLevel += points;
        ApplyStats();
        shopUI?.UpdateUI();
        SaveStats();
    }

    public void UpgradeAttack(int points)
    {
        attackUpgradeLevel += points;
        ApplyStats();
        shopUI?.UpdateUI();
        SaveStats();
    }

    public void SetMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = Mathf.Clamp(newSpeed, 1f, 20f);
        Debug.Log($"[PlayerStatsCollector] Setting move speed to: {currentMoveSpeed}");

        if (playerMovement2 != null)
        {
            playerMovement2.SetBaseSpeed(currentMoveSpeed);
        }
    }

    public void SetDamage(int newDamage)
    {
        currentDamage = newDamage;
        if (playerAttack != null)
        {
            playerAttack.damage = currentDamage;
        }
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        Currency.instance?.SetCurrency(currency);
        SaveStats();
    }

    public bool SpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            Currency.instance?.SetCurrency(currency);
            SaveStats();
            return true;
        }
        return false;
    }

    public bool TryPurchaseUpgrade() => SpendCurrency(upgradeCost);

    private void ApplyStats()
    {
        float calculatedSpeed = baseMoveSpeed + speedUpgradeLevel * speedBonusPerLevel;
        Debug.Log($"[PlayerStatsCollector] ApplyStats - baseMoveSpeed: {baseMoveSpeed}, bonus: {speedUpgradeLevel * speedBonusPerLevel}, total: {calculatedSpeed}");
        
        SetMoveSpeed(calculatedSpeed);
        SetDamage(baseDamage + attackUpgradeLevel);
    }

    public void SaveStats()
    {
        PlayerStatsData data = new PlayerStatsData
        {
            moveSpeed = currentMoveSpeed,
            damage = currentDamage,
            currency = currency,
            speedUpgradeLevel = speedUpgradeLevel,
            attackUpgradeLevel = attackUpgradeLevel,
        };

        File.WriteAllText(saveFilePath, JsonUtility.ToJson(data));
        Debug.Log("[PlayerStatsCollector] Stats saved.");
    }

    public bool LoadStats()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerStatsData data = JsonUtility.FromJson<PlayerStatsData>(json);

            currentMoveSpeed = data.moveSpeed;
            currentDamage = data.damage;
            currency = data.currency;
            speedUpgradeLevel = data.speedUpgradeLevel;
            attackUpgradeLevel = data.attackUpgradeLevel;

            Currency.instance?.SetCurrency(currency);
            Debug.Log($"[PlayerStatsCollector] Stats loaded. moveSpeed: {currentMoveSpeed}");
            return true;
        }
        else
        {
            Debug.Log("[PlayerStatsCollector] No saved stats found. Using defaults.");
            return false;
        }
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }

    [System.Serializable]
    private class PlayerStatsData
    {
        public float moveSpeed = 10f; // Default value
        public int damage = 1;        // Default value
        public int currency;
        public int speedUpgradeLevel;
        public int attackUpgradeLevel;
        public float positionX;
        public float positionY;
        public float positionZ;
    }
}