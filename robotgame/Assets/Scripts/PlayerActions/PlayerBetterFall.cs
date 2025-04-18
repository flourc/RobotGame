using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerBetterFall : MonoBehaviour {

      public float fallMultiplier = 3.5f;
      public float lowJumpMultiplier = 3f;
      Rigidbody rb;

       void Awake(){
             rb = GetComponent<Rigidbody> ();
       }

       void FixedUpdate(){
             if (rb.velocity.y < 0) {
                   rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
             } else if (rb.velocity.y > 0 && !Input.GetButton ("Jump")){
                   rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
             }
       }
}
