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
    public int shootState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = .01f;
        visionRadius = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 target = new Vector3(player.position.x, 
                                       transform.position.y, 
                                       player.position.z);


        float dist = Vector3.Distance(player.position, transform.position);
        while (dist < visionRadius && dist > 5) {

            if (!active) {
                print("invoking shoot");
                InvokeRepeating("shoot", 0f, 1f);
                active = true;
            }


            transform.LookAt(target);
            transform.Rotate(new Vector3(0f, 90f, 0f));//TEMP bc i cant import stuff properly

            Vector3 positionChange = target - transform.position;

            rb.AddForce(positionChange * .1f);

        }
    }

 
    public void damage() {
        rb.AddForce(-transform.forward * 10f, ForceMode.Impulse);
    }

    public void shoot() {
        if (shootState == 0) {
            shootHelper("left", leftShoot); 
            shootState++;
        }
        else if (shootState == 1) {
            shootHelper("right", rightShoot);
            shootState--;
        }
        
    }

    void shootHelper (string direction, Transform t) {
        if (direction == "left") {
            anim.Play("left_fire");
        }
        else if (direction == "right") {
            anim.Play("right_fire");
        }
        // else {
        //     anim.Play("both_fire");
        // }
        

        GameObject cb = Instantiate(ball, leftShoot.position, transform.rotation);
        Vector3 shootDir = transform.forward + new Vector3(-100f, 0f, 0f);
        cb.GetComponent<Rigidbody>().velocity = -t.right * 30;
    }  



}
