using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed;
    public int health;
    public float attackPower;
    public float visionRadius;
    public Transform player;

    void Start()
    {
        moveSpeed = .2f;
        health = 6;
        attackPower = .5f;
        visionRadius = 15f;
    }

    void Update()
    {
        // Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        // Vector3 playerXZ = Vector3.ProjectOnPlane(player.position, Vector3.up);
        // Vector3 myXZ = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        // float dist = Vector3.Distance(playerXZ, myXZ);
        // if (dist < visionRadius) {
        //     Vector3 direction = new Vector3(playerXZ.x - myXZ.x, transform.position.y, playerXZ.z - myXZ.z);
        //     direction = Vector3.Normalize(direction);
        //     transform.Translate(direction * Time.deltaTime * moveSpeed);
        // }

        Vector3 target = new Vector3(player.position.x, 
                                       transform.position.y, 
                                       player.position.z);


        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < visionRadius) {
            transform.LookAt(player);
            transform.Rotate(new Vector3(-90f, 0f, 0f));//TEMP bc i cant import stuff properly
            transform.position = Vector3.MoveTowards(transform.position, target, .01f);
        
            // direction = Vector3.Normalize(direction); 
            // transform.Translate(direction * Time.deltaTime * moveSpeed);
        }

    }
}
