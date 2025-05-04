using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public int damage = 1;
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

    private void OnCollisionEnter (Collision collider) 
    {

        if (this.gameObject.CompareTag("weapon")) {
            if (collider.gameObject.tag != "Player") {
                if (collider.gameObject.GetComponent<EntityHealth>() != null) {
                    collider.gameObject.GetComponent<EntityHealth>().TakeDamage(damage);
                }
                print("destroying");
                Destroy (gameObject);
            }
            
        }
        else {
            if (collider.gameObject.tag != "enemy") {
                if (collider.gameObject.GetComponent<EntityHealth>() != null) {
                collider.gameObject.GetComponent<EntityHealth>().TakeDamage(damage);
                }
                Destroy (gameObject);
            }
        }
        

    }
}
