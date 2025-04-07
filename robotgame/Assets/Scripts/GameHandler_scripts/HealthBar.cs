using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject entity;
    public int health;
    public int startingHealth;
    public float barFill;
    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = 10;
        barFill = 1.0f;
        health = startingHealth;
        healthBar.SetActive(true);
    }

    void Update()
    {
       
    }

    public void GetHit(int dmg)
    {
        health -= dmg;
        if (health < 0) {
            health = 0;
        }
        barFill = (float)health / startingHealth;
        updateStatsDisplay();
        if (health <= 0) {
            gameObject.BroadcastMessage("LoseScreen");
        }
    }

    public void updateStatsDisplay()
    {
        healthBar.GetComponent<Image>().fillAmount = barFill;
    }

}
