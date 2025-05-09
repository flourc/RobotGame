using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public int damage = 1;
    public GameObject handler;
    private HealthBar playerHP;
    private Vector3 startPosition;
    public float maxDistance = 80f;

    // Start is called before the first frame update
    void Start()
    {
        handler = GameObject.FindWithTag("GameController");
        playerHP = handler.GetComponent<HealthBar>();
        startPosition = transform.position;
        //Debug.Log("Bullet tag at start: " + gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled > maxDistance) {
            Destroy(gameObject); // Bullet has traveled too far, destroy it
        }
    }

    private void OnCollisionEnter (Collision collider) 
    {

        if (this.gameObject.CompareTag("weapon")) {
            //Debug.Log("player bullet hit!");
            if (collider.gameObject.tag != "Player") {
                if (collider.gameObject.GetComponent<EntityHealth>() != null) {
                    collider.gameObject.GetComponent<EntityHealth>().TakeDamage(damage);
                }
                print("destroying");
                Destroy (gameObject);
            }
            
        }
        else if (this.gameObject.CompareTag("weapon_enemy")) {
            //Debug.Log("enemy bullet hit!");
            if (collider.gameObject.tag != "enemy") {
                if (collider.gameObject.GetComponent<EntityHealth>() != null) {
                collider.gameObject.GetComponent<EntityHealth>().TakeDamage(damage);
                }
                Destroy (gameObject);
            }
        }
        

    }
}
