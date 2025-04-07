using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelOneHandler : MonoBehaviour
{
    public GameObject slasherOnGround;
    public GameObject droneOnGround; //<== make this
    public GameObject slasherArm;
    public GameObject gunArm;
    public float droneDistance;

    public static string arm;

    public Transform player;
    public GameObject info;

    public GameObject door;
    public GameObject doorInfo;
    public float distance;

    
    public float doorDistance;
    public Material doorLight;
    public bool hasKeyCard;
    public bool isOpen;
    public GameObject keyPad;

    // public AudioSource audios;

    // Start is called before the first frame update
    void Start()
    {
        hasKeyCard = true;
        slasherArm.SetActive(false);
        gunArm.SetActive(false);


        info.SetActive(false);
        doorInfo.SetActive(false);
        doorLight.SetColor("_EmissionColor", Color.red);
        arm = "none";
        // audios = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, slasherOnGround.transform.position);
        if (distance < 5 && arm == "none" && !info.activeSelf) {
            info.SetActive(true);
        }
        else if (distance >= 5 && info.activeSelf){
            info.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E) && distance < 5 && slasherOnGround.activeSelf) {
            slasherOnGround.SetActive(false);
            gunArm.SetActive(false);
            slasherArm.SetActive(true);
            info.SetActive(false);
            arm = "slasher";
            // audios.Play();
        }

        droneDistance = Vector3.Distance(player.position, droneOnGround.transform.position);
        if (droneDistance < 5 && arm == "none" && !info.activeSelf) {
            info.SetActive(true);
        }
        else if (distance >= 5 && info.activeSelf){
            info.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E) && droneDistance < 5 && droneOnGround.activeSelf) {
            // droneOnGround.SetActive(false);
            gunArm.SetActive(true);
            info.SetActive(false);
            slasherArm.SetActive(false);
            arm = "gun";
            // audios.Play();
        }



        // doorDistance = distance = Vector3.Distance(player.position, door.transform.position);
        // if (doorDistance <= 2) {
        //     if (hasKeyCard) {
        //         openDoor();
        //     }
        // }
        // if (doorDistance <= 6) {
        //     doorInfo.SetActive(true);
        // }        
        // if (doorDistance >= 6 && doorInfo.activeSelf) {
        //     doorInfo.SetActive(false);
        // }

        
    }

    // public void openDoor() {
    //     doorLight.SetColor("_EmissionColor", Color.green);
    //     InvokeRepeating("incrementDoor", 0f, .3f);
    // }

    // public void incrementDoor() {
    //     door.transform.Translate(new Vector3(0f, 0.4f, 0f));
    //     if (keyPad.transform.position.y >= 4 && keyPad.activeSelf) {
    //         keyPad.SetActive(false);
    //     }
    //     if (door.transform.position.y >= 14) {
    //         CancelInvoke();
    //     }
    // }
    
}
