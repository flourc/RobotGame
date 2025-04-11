using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour

{
    public float pushForce = 1f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "pushable")
        {
            Rigidbody box = hit.collider.GetComponent<Rigidbody>();
            if (box != null)
            {
                box.velocity = Vector3.zero; // Stop the existing velocity (prevents unwanted rotation effects)
                box.AddForce(transform.forward * pushForce, ForceMode.Impulse);

                // Prevent rotation by zeroing out angular velocity
                box.angularVelocity = Vector3.zero;
            }
        }
    }
}
