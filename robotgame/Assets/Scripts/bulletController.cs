using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public int damage;
    public GameObject handler;
    private HealthBar playerHP;
    // Start is called before the first frame update
    void Start()
    {
        handler = GameObject.FindWithTag("GameController");
        playerHP = handler.GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider collider) 
    {
        Destroy (gameObject);
        if (collider.gameObject.CompareTag("Player")) {
            playerHP.TakeDamage(damage);
        } else if (collider.gameObject.CompareTag("enemy")) {
            collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

    }
}
