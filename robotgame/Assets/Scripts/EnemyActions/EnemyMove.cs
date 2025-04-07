using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed;
    public int health;
    public float attackPower;
    public float visionRadius = 2.5f;

    void Start()
    {
        
    }

    void Update()
    {
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Vector3 playerXZ = Vector3.ProjectOnPlane(player.position, Vector3.up);
        Vector3 myXZ = Vector3.ProjectOnPlane(transform.position, Vector3.up);
        float dist = Vector3.Distance(playerXZ, myXZ);
        if (dist < visionRadius) {
            Vector3 direction = new Vector3(playerXZ.x - myXZ.x, transform.position.y, playerXZ.z - myXZ.z);
            direction = Vector3.Normalize(direction);
            transform.Translate(direction * Time.deltaTime * moveSpeed);
        }
    }
}
