using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed;
    public float visionRadius;
    public Transform player;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = .1f;
        visionRadius = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 target = new Vector3(player.position.x, 
                                       transform.position.y, 
                                       player.position.z);


        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < visionRadius) {
            transform.LookAt(target);

            Vector3 positionChange = target - transform.position;
            // rb.velocity = positionChange * .1f;
            rb.AddForce(positionChange * .1f);
        
            transform.position = Vector3.MoveTowards(transform.position, target, .01f);
            anim.SetBool("Walking", true);

            if (dist < 3 && !anim.GetCurrentAnimatorStateInfo(0).IsName("bite")) {
                anim.Play("bite");
            }
        }
        else {
            anim.SetBool("Walking", false);
        }
    }
 
    public void damage() {
        rb.AddForce(-transform.forward * 10f, ForceMode.Impulse);
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
    }
}
