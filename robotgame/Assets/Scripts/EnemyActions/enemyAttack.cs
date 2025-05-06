using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public GameObject player;
    public int attackPower = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter (Collision collider) 
    {
        if (collider.gameObject.tag == "Player") {
           InvokeRepeating("damagePlayer", 0f, 1f);

        }
    }
    private void OnCollisionExit (Collision collider) {
        if (collider.gameObject.tag == "Player") {
            CancelInvoke("damagePlayer");
        }
    }

    public void damagePlayer()
    {
        player.GetComponent<EntityHealth>().TakeDamage(attackPower);
    }

}
