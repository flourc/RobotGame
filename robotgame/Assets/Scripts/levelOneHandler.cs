using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelOneHandler : MonoBehaviour
{
    // public GameObject slasherOnGround;
    // public GameObject droneOnGround; //<== make this
    // public GameObject slasherArm;
    // public GameObject gunArm;
    // public float droneDistance;

    // public Transform player;
    // public GameObject info;

    // public float distance;
    // public float atkDistance;
    // public GameObject rubble;
    // public GameObject atkInfo;

    // public float holdDistance;
    // public GameObject crate;
    // public GameObject holdInfo;

    // public EnemyHealth eh;
    // public bool holdShown;

    // public GameObject keypad;

    // public GameObject creep;
    // public Transform doorTwo;
    // public GameObject keyInfo;
    // public float keyDistance;
    
    public GameObject crosshair;
    public KeypadUI kp;

    public GameObject droneOnGround; //<== make this
    public GameObject drone;
    public GameObject gunArm;
    public GameObject swordArm;
    public float droneDistance;
    public Transform player;

    public deathFloor df;
    public GameObject box;

    public GameObject equipInfo;
    public GameObject doorInfo;
    public GameObject door;
    public float doorDistance;

    // public AudioSource audios;

    // Start is called before the first frame update
    void Start()
    {
        equipInfo.SetActive(false);
        crosshair.SetActive(false);

        swordArm.SetActive(true);
        gunArm.SetActive(false);
        droneOnGround.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        droneDistance = Vector3.Distance(player.position, droneOnGround.transform.position);
        doorDistance = Vector3.Distance(player.position, door.transform.position);

        if (drone == null) {
            droneOnGround.SetActive(true);
        }

        if (droneDistance<7 && droneOnGround.activeSelf && !gunArm.activeSelf) {
            equipInfo.SetActive(true);
        }
        else if (droneDistance > 7) {
            equipInfo.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E) && droneDistance < 7 && droneOnGround.activeSelf) {
            gunArm.SetActive(true);
            swordArm.SetActive(false);
            equipInfo.SetActive(false);
        }

        if (box == null) {
            //Debug.Log("FLOOR SHOULD BE OFF");
            df.turnOff();
        }


        if (doorDistance < 7 && !doorInfo.activeSelf) {
            doorInfo.SetActive(true);
        }
        else if (doorDistance > 7 && doorInfo.activeSelf) {
            doorInfo.SetActive(false);
        }


        
    }

    
}