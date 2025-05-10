using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour

{
    public float pushForce = 1f;

	void Start(){
		//added start so the script can be enabled/disabled in the Inspector
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Only push tagged objects
        if (hit.gameObject.CompareTag("pushable"))
        {
            Rigidbody box = hit.collider.attachedRigidbody;

            // Make sure the object has a rigidbody and it's not kinematic
            if (box == null || box.isKinematic)
                return;

            // Don't push objects below or above the character (e.g. jumping on top of a box)
            if (hit.moveDirection.y < -0.3f)
                return;

            // Calculate push direction (ignore vertical component)
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            // Apply the push (instant burst or tweak for smoother effect)
            box.velocity = pushDir * pushForce;
        }
    }
}
