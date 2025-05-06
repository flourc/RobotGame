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
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private int currentDamage;
    [SerializeField] private int currency = 0;

    private float savedPosX, savedPosY, savedPosZ;

    [Header("Base Stats")]
    [SerializeField] private float baseMoveSpeed = 7f;
    [SerializeField] private int baseDamage = 1;

    [Header("Upgrades")]
    [SerializeField] private int speedUpgradeLevel = 0;
    [SerializeField] private int attackUpgradeLevel = 0;
    [SerializeField] private float speedBonusPerLevel = 0.5f;
    [SerializeField] private int upgradeCost = 10;

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
            return;
        }

        saveFilePath = Path.Combine(Application.persistentDataPath, "playerStats.json");
    }
    
#if UNITY_EDITOR
    public static void ResetFromEditor()
    {
        PlayerStatsCollector temp = new PlayerStatsCollector(); // Or access existing instance
        temp.ResetSavedPlayerPosition();
    }
#endif

    private IEnumerator Start()
    {
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

        LoadStats();

        baseMoveSpeed = playerMovement2 != null ? playerMovement2.baseSpeed : baseMoveSpeed;
        baseDamage = playerAttack != null ? playerAttack.damage : baseDamage;

        ApplyStats();
        UpdateUI();
    }

    private void UpdateUI() => shopUI?.UpdateUI();

    public float GetCurrentMoveSpeed() => currentMoveSpeed;
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
        SetMoveSpeed(baseMoveSpeed + speedUpgradeLevel * speedBonusPerLevel);
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
            positionX = savedPosX,
            positionY = savedPosY,
            positionZ = savedPosZ
        };

        File.WriteAllText(saveFilePath, JsonUtility.ToJson(data));
        Debug.Log("[PlayerStatsCollector] Stats saved.");
    }

    public void LoadStats()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerStatsData data = JsonUtility.FromJson<PlayerStatsData>(json);

            currentMoveSpeed = data.moveSpeed;
            currentDamage = data.damage;
            currency = data.currency;
            savedPosX = data.positionX;
            savedPosY = data.positionY;
            savedPosZ = data.positionZ;
            speedUpgradeLevel = data.speedUpgradeLevel;
            attackUpgradeLevel = data.attackUpgradeLevel;

            Currency.instance?.SetCurrency(currency);
            Debug.Log("[PlayerStatsCollector] Stats loaded.");
        }
        else
        {
            Debug.Log("[PlayerStatsCollector] No saved stats found. Using defaults.");
        }
    }

    private void OnApplicationQuit()
    {
        SaveStats();
        ResetSavedPlayerPosition();
    }

    public void ResetSavedPlayerPosition() => SavePlayerPosition(Vector3.zero);

    public void SavePlayerPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("PlayerPosX", position.x);
        PlayerPrefs.SetFloat("PlayerPosY", position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", position.z);
        PlayerPrefs.Save();
    }

    public Vector3 GetSavedPosition() => new Vector3(savedPosX, savedPosY, savedPosZ);

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
}
