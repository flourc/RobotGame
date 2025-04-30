using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class AttackShopUI : MonoBehaviour
{
    public static AttackShopUI instance;
    public TMP_Text AttackValueText;
    private int playerAttack = 0;
    
    // Reference to the stats collector
    private PlayerStatsCollector statsCollector;

    void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the stats collector
        statsCollector = PlayerStatsCollector.instance;
        
        // Initialize with current value from player stats if available
        if (statsCollector != null)
        {
            playerAttack = statsCollector.GetAttackUpgradeLevel();
        }
        
        UpdateDisplayText();
    }

    public void upgradeAttack(int attackPoints)
    {
        playerAttack += attackPoints;
        
        // If we have a stats collector, apply the upgrade there too
        if (statsCollector != null && attackPoints > 0)
        {
            statsCollector.UpgradeAttack(attackPoints);
        }
        
        UpdateDisplayText();
    }
    
    private void UpdateDisplayText()
    {
        // Update the UI text
        AttackValueText.text = playerAttack.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally sync with player stats every frame for consistency
        if (statsCollector != null)
        {
            int currentAttackLevel = statsCollector.GetAttackUpgradeLevel();
            if (playerAttack != currentAttackLevel)
            {
                playerAttack = currentAttackLevel;
                UpdateDisplayText();
            }
        }
    }
}