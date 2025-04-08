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

    public Transform player;
    public GameObject info;

    public GameObject door;
    public float distance;

    public EnemyHealth eh;
    

    // public AudioSource audios;

    // Start is called before the first frame update
    void Start()
    {
        slasherArm.SetActive(false);
        gunArm.SetActive(false);
        droneOnGround.SetActive(false);

        info.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (!eh.returnAlive()) {
            droneOnGround.SetActive(true);
            door.SetActive(false);
        }

        distance = Vector3.Distance(player.position, slasherOnGround.transform.position);
        if (distance < 7 && !slasherArm.activeSelf && !info.activeSelf) {
            info.SetActive(true);
        }
        else if (distance >= 7 && info.activeSelf){
            info.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E) && distance < 7 && slasherOnGround.activeSelf) {
            slasherOnGround.SetActive(false);
            gunArm.SetActive(false);
            slasherArm.SetActive(true);
            info.SetActive(false);
            // audios.Play();
        }

        droneDistance = Vector3.Distance(player.position, droneOnGround.transform.position);
        if (droneDistance < 7 && !gunArm.activeSelf && !info.activeSelf && droneOnGround.activeSelf) {
            info.SetActive(true);
        }
        else if (distance >= 7 && info.activeSelf){
            info.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E) && droneDistance < 7 && droneOnGround.activeSelf) {
            gunArm.SetActive(true);
            info.SetActive(false);
            slasherArm.SetActive(false);
        }


        
    }

}
