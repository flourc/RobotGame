using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class SpeedShopUI : MonoBehaviour
{
    public static SpeedShopUI instance;
    public TMP_Text SpeedValueText;
    private int playerSpeed = 0;
    
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
            playerSpeed = statsCollector.GetSpeedUpgradeLevel();
        }
        
        UpdateDisplayText();
    }

    public void UpgradeSpeed(int speedPoints)
    {
        playerSpeed += speedPoints;
        
        // If we have a stats collector, apply the upgrade there too
        if (statsCollector != null && speedPoints > 0)
        {
            statsCollector.UpgradeSpeed(speedPoints);
        }
        
        UpdateDisplayText();
    }
    
    private void UpdateDisplayText()
    {
        // Update the UI text
        SpeedValueText.text = playerSpeed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally sync with player stats every frame for consistency
        if (statsCollector != null)
        {
            int currentSpeedLevel = statsCollector.GetSpeedUpgradeLevel();
            if (playerSpeed != currentSpeedLevel)
            {
                playerSpeed = currentSpeedLevel;
                UpdateDisplayText();
            }
        }
    }
}