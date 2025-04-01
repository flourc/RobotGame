using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "floor" || collision.gameObject.tag == "Player")
        {
            Destroy (gameObject);
        //TODO: set bullet to enemy tag
        }   
    }
}
