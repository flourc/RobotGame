using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMove : MonoBehaviour
{


    private int rotateSpeed;
    private int moveSpeed;
    private bool rotating;
    private bool backingUp;
    
    void Start()
    {
        rotating = false;
        backingUp = false;
        moveSpeed = 1;
        rotateSpeed = 30;
    }

    void Update()
    {
        if (rotating) 
        {

        } else if (backingUp)
        {

        } else {
            GoForward();
        }
        
    }

    void GoForward()
    {
        transform.position += (Vector3.forward * Time.deltaTime * moveSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        transform.position += (Vector3.forward * -1 * Time.deltaTime * moveSpeed);
        transform.Rotate(new Vector3 (0, rotateSpeed, 0) * Time.deltaTime);
    }
}
