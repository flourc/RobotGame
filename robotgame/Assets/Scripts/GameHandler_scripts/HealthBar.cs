using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : EntityHealth
{
    public float barFill;
    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 10;
        barFill = 1.0f;
        currHealth = maxHealth;
        healthBar.SetActive(true);
    }

    void Update()
    {
        barFill = (float)currHealth / maxHealth;
        updateStatsDisplay();
    }

    public override void OnDeath()
    {
        GameHandler gameHandler = FindObjectOfType<GameHandler>();
        if (gameHandler != null)
        {
            gameHandler.LoseScreen();
        }
        //gameObject.BroadcastMessage("LoseScreen");
    }

    public void updateStatsDisplay()
    {
        healthBar.GetComponent<Image>().fillAmount = barFill;
    }

}
