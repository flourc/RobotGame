using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public bool alive;

    public GameObject myMesh;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amt) 
    {
        currHealth -= amt;
        if (currHealth <= 0) {
            currHealth = 0;
            alive = false;
            myMesh.SetActive(false);
        }
    }
}
