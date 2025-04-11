using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMove : MonoBehaviour
{

    //                          IMPORTANT NOTE!!!!!! 
    // the floor of a level MUST have the tag "floor" or otherwise the roomba 
    // will constantly be colliding with the floor and turning around

    public bool hostile;
    public GameObject handler;
    public Transform player;
    private int rotateSpeed;
    private int moveSpeed;
    private bool rotating;
    private int framesInMotion;
    private bool backingUp;
    private bool waiting;
    private const int ROTATE_FRAMES = 50;
    private const int BACK_FRAMES = 25;
        // Vector3 playerXZ = Vector3.ProjectOnPlane(player.position, Vector3.up);
        // Vector3 myXZ = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        // float dist = Vector3.Distance(playerXZ, myXZ);


    void Start()
    {
        hostile = false;
        rotating = false;
        backingUp = false;
        waiting = false;
        moveSpeed = 1;
        rotateSpeed = 30;
        framesInMotion = 0;
    }

    void FixedUpdate()
    {
        hostile = PlayerInRange();
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
        if (hostile) {
            FacePlayer();
            rotating = false;
        } else {
            transform.Rotate(new Vector3 (0, 0, rotateSpeed) * Time.deltaTime);
            framesInMotion++;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (hostile && other.gameObject.tag == "Player") {
            handler.GetComponent<HealthBar>().TakeDamage(1);
        }
        if (other.gameObject.tag != "floor") {
            StartCoroutine(Waiting());
            backingUp = true;
        }   
    }

    void FacePlayer()
    {
        transform.LookAt(player);
        transform.Rotate(new Vector3(-90f, 0f, 0f));
    }

    bool PlayerInRange()
    {
        Vector3 playerXZ = Vector3.ProjectOnPlane(player.position, Vector3.up);
        Vector3 myXZ = Vector3.ProjectOnPlane(transform.position, Vector3.up);
        float dist = Vector3.Distance(playerXZ, myXZ);
        return dist <= 5f;
    }


    IEnumerator Waiting()
    {
        waiting = true;
        yield return new WaitForSeconds(1.0f);
        waiting = false;

    }
}
