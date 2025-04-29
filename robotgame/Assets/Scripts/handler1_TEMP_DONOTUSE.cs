using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveloneHandler_TEMP_DONOTUSE : MonoBehaviour
{
    public GameObject droneOnGround; //<== make this
    public GameObject drone;
    public GameObject gunArm;
    public GameObject swordArm;
    public float droneDistance;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        swordArm.SetActive(true);
        gunArm.SetActive(false);
        droneOnGround.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        droneDistance = Vector3.Distance(player.position, droneOnGround.transform.position);
        
        if (drone == null) {
            droneOnGround.SetActive(true);

        }

        if (Input.GetKey(KeyCode.E) && droneDistance < 7 && droneOnGround.activeSelf) {
            gunArm.SetActive(true);
            swordArm.SetActive(false);
        }
        
    }
}
