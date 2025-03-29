using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMove : MonoBehaviour
{

    //                          IMPORTANT NOTE!!!!!! 
    // the floor of a level MUST have the tag "floor" or otherwise the roomba 
    // will constantly be colliding with the floor and turning around
    
    private int rotateSpeed;
    private int moveSpeed;
    private bool rotating;
    private int framesInMotion;
    private bool backingUp;
    private bool waiting;
    private const int ROTATE_FRAMES = 1000;
    private const int BACK_FRAMES = 500;


    void Start()
    {
        rotating = false;
        backingUp = false;
        waiting = false;
        moveSpeed = 1;
        rotateSpeed = 60;
        framesInMotion = 0;
    }

    void Update()
    {
        if (!waiting){
            if (rotating) {
                if (framesInMotion > ROTATE_FRAMES) {
                    StartCoroutine(Waiting());
                    rotating = false;
                    framesInMotion = 0;
                } else {
                    Rotating();
                }
            } else if (backingUp) {
                if (framesInMotion > BACK_FRAMES) {
                    StartCoroutine(Waiting());
                    rotating = true;
                    backingUp = false;
                    framesInMotion = 0;
                } else {
                    GoBackward();
                }
            } else {
                GoForward();
            }
        }
    }

    void GoForward()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
    }

    void GoBackward()
    {
        transform.Translate(Vector3.down * -1 * Time.deltaTime * moveSpeed);
        framesInMotion++;
    }

    void Rotating()
    {
        transform.Rotate(new Vector3 (0, 0, rotateSpeed) * Time.deltaTime);
        framesInMotion++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "floor") {
            StartCoroutine(Waiting());
            backingUp = true;
        }   
    }


    IEnumerator Waiting()
    {
        waiting = true;
        yield return new WaitForSeconds(1.0f);
        waiting = false;

    }
}
