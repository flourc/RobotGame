using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public int value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player (you can tag the player as "Player")
        if (other.CompareTag("Player"))
        {
            // Add 1 to the player's currency
            Currency.instance.AddCurrency(value);

            // Destroy this collectible object
            Destroy(gameObject);
        }
    }


}
