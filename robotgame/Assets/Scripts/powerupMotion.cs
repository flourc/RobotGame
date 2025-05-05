using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupMotion : MonoBehaviour
{
    public PlayerMovement2 player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").
                                GetComponentInChildren<PlayerMovement2>();
        transform.Rotate(new Vector3(-15f, 0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, -0.5f, 0f));
    }

    void OnCollisionEnter(Collision other)
    {
        player.jumpForce += 5f;
        Destroy(gameObject);
    }
}
