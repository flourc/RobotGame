using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMove : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed;
    public float visionRadius;
    public Transform player;
    public Transform leftShoot;
    public Transform rightShoot;

    public Rigidbody rb;

    public GameObject ball;
    public bool active = false;
    public bool shootState;

    public Transform endWall;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = .01f;
        visionRadius = 20f;
        shootState = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 target = new Vector3(player.position.x, 
                                       transform.position.y, 
                                       player.position.z);

        float dist = Vector3.Distance(player.position, transform.position);
        
        if (dist < visionRadius && dist > 5) {
            if (!active) {
                print("invoking shoot");
                InvokeRepeating("shoot", 0f, .5f);
                active = true;
            }


            transform.LookAt(target);
            transform.Rotate(new Vector3(0f, 90f, 0f));//TEMP bc i cant import stuff properly

            // Vector3 positionChange = target - transform.position;
            // rb.AddForce(positionChange * .1f);
            // transform.Translate(positionChange * .1f);
            transform.position = Vector3.MoveTowards(transform.position, target, .003f);

        }
        else if (dist < 5) {
            target = endWall.position;
            transform.position = Vector3.MoveTowards(transform.position, target, .003f);
        }
    }

    public void shoot() {
        if (shootState) {
            shootHelper("left", leftShoot); 
            shootState = false;
        }
        else {
            shootHelper("right", rightShoot);
            shootState = true;
        }
        
    }

    void shootHelper (string direction, Transform t) {
        if (direction == "left") {
            anim.Play("left_fire");
        }
        else {
            anim.Play("right_fire");
        }

        GameObject cb = Instantiate(ball, t.position, transform.rotation);
        Vector3 shootDir = transform.forward + new Vector3(-100f, 0f, 0f);
        cb.GetComponent<Rigidbody>().velocity = -t.right * 30;
    }  



}
