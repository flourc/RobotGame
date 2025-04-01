using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    public float sphereRadius = 0.2f; // Radius for the SphereCast - adjust based on how small objects are
    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""
    }

    void Update()
    {
        // Draw debug ray to visualize the pickup range
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickUpRange, Color.red);
        
        // Draw debug sphere to visualize the SphereCast radius at the maximum range
        Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * pickUpRange, Color.red);
        DebugDrawSphere(transform.position + transform.TransformDirection(Vector3.forward) * pickUpRange, sphereRadius, Color.yellow);

        if (Input.GetKeyDown((KeyCode.Mouse1))) //pickup with right click
        {
            if (heldObj == null) //if currently not holding anything
            {
                //perform spherecast to check if player is looking at object within pickuprange
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, sphereRadius, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        //pass in object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                        
                        // Debug info - show what was picked up
                        Debug.Log("Picked up: " + hit.transform.gameObject.name + " at distance: " + hit.distance);
                    }
                    else
                    {
                        // Debug info - object hit but wrong tag
                        Debug.Log("Hit: " + hit.transform.gameObject.name + " but it doesn't have the canPickUp tag");
                    }
                }
                else
                {
                    // Debug info - nothing hit
                    Debug.Log("No object detected within range");
                }
            }
            else
            {
                if(canDrop == true)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropObject();
                }
            }
        }
        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            RotateObject();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                StopClipping();
                ThrowObject();
            }
        }
    }

    // Helper method to draw a sphere in the editor for debugging
    void DebugDrawSphere(Vector3 position, float radius, Color color)
    {
        // Draw circle in XZ plane
        int segments = 16;
        float angleStep = 360f / segments;
        
        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * angleStep * Mathf.Deg2Rad;
            float angle2 = (i + 1) * angleStep * Mathf.Deg2Rad;
            
            Vector3 pos1 = position + new Vector3(Mathf.Sin(angle1) * radius, 0, Mathf.Cos(angle1) * radius);
            Vector3 pos2 = position + new Vector3(Mathf.Sin(angle2) * radius, 0, Mathf.Cos(angle2) * radius);
            
            Debug.DrawLine(pos1, pos2, color);
            
            // Draw some lines in vertical plane
            pos1 = position + new Vector3(Mathf.Sin(angle1) * radius, Mathf.Cos(angle1) * radius, 0);
            pos2 = position + new Vector3(Mathf.Sin(angle2) * radius, Mathf.Cos(angle2) * radius, 0);
            
            Debug.DrawLine(pos1, pos2, color);
            
            // Another vertical plane (XY)
            pos1 = position + new Vector3(0, Mathf.Sin(angle1) * radius, Mathf.Cos(angle1) * radius);
            pos2 = position + new Vector3(0, Mathf.Sin(angle2) * radius, Mathf.Cos(angle2) * radius);
            
            Debug.DrawLine(pos1, pos2, color);
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    
    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    
    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }
    
    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
        {
            canDrop = false; //make sure throwing can't occur during rotating

            //disable player being able to look around
            //mouseLookScript.verticalSensitivity = 0f;
            //mouseLookScript.lateralSensitivity = 0f;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            //rotate the object depending on mouse X-Y Axis
            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            //re-enable player being able to look around
            //mouseLookScript.verticalSensitivity = originalvalue;
            //mouseLookScript.lateralSensitivity = originalvalue;
            canDrop = true;
        }
    }
    
    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    
    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }
}