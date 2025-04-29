using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadUI : UI_Layer_base
{
    public GameObject keypadObj;
    public float keypadInteractRadius;
    public Transform player;

    public override void Init()
    {
        keypadInteractRadius = 8;
        LayerOff();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerNearby() && Input.GetKeyDown(KeyCode.E)) {
            LayerOn();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SendMessageUpwards("DeactivateLayers");
        }
    }

    bool playerNearby()
    {

        Vector3 playerXZ = Vector3.ProjectOnPlane(player.position, Vector3.up);
        Transform keypadLoc = keypadObj.GetComponent<Transform>();
        Vector3 keypadXZ = 
                        Vector3.ProjectOnPlane(keypadLoc.position, Vector3.up);
        float dist = Vector3.Distance(player.position, keypadLoc.position);
        
        return (Vector3.Distance(playerXZ, keypadXZ) <= keypadInteractRadius);
    }
}
