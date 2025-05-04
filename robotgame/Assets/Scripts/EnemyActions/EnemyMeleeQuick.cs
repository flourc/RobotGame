using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeQuick : MonoBehaviour
{
    public GameObject player;
    public int attackPower = 1;
    public float attackCooldown = 0.7f; // Time between attacks in seconds
    private float lastAttackTime = 0f;

    void Start()
    {
        lastAttackTime = Time.time; // Initialize to start with an attack possibility
    }

    void Update()
    {
        // Continuously check if the player is colliding and if enough time has passed since the last attack
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (IsPlayerColliding())
            {
                AttackPlayer();
                lastAttackTime = Time.time; // Reset the last attack time after the attack
            }
        }
    }

    private bool IsPlayerColliding()
    {
        // Check if player is in range of the enemy
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance to player: " + distance);
        return distance < 3f; // Adjust the 2f to the desired range
    }

    private void AttackPlayer()
    {
        player.GetComponent<EntityHealth>().TakeDamage(attackPower);
    }
}
