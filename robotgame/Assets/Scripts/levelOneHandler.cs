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

    // public AudioSource audios;

    // Start is called before the first frame update
    void Start()
    {
        // info.SetActive(false);
        // atkInfo.SetActive(false);
        // holdInfo.SetActive(false);
        // keyInfo.SetActive(false);
        // holdShown = false;
        crosshair.SetActive(false);

        swordArm.SetActive(true);
        gunArm.SetActive(false);
        droneOnGround.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        droneDistance = Vector3.Distance(player.position, droneOnGround.transform.position);
        
        if (drone == null) {
            droneOnGround.SetActive(true);

        }

        if (Input.GetKey(KeyCode.E) && droneDistance < 7 && droneOnGround.activeSelf) {
            gunArm.SetActive(true);
            swordArm.SetActive(false);
        }

        if (box == null) {
            df.turnOff();
        }
        
    }




        // if (!eh.returnAlive()) {
        //     droneOnGround.SetActive(true);
        // } 

        // distance = Vector3.Distance(player.position, slasherOnGround.transform.position);
        // if (distance < 7 && !slasherArm.activeSelf && !info.activeSelf) {
        //     info.SetActive(true);
        // }
        // else if (distance >= 7 && info.activeSelf){
        //     info.SetActive(false);
        // }

        // if (Input.GetKey(KeyCode.E) && distance < 7 && slasherOnGround.activeSelf) {
        //     slasherOnGround.SetActive(false);
        //     gunArm.SetActive(false);
        //     slasherArm.SetActive(true);
        //     info.SetActive(false);
        //     // audios.Play();
        // }

    //     droneDistance = Vector3.Distance(player.position, droneOnGround.transform.position);
    //     if (droneDistance < 7 && !gunArm.activeSelf && !info.activeSelf && droneOnGround.activeSelf) {
    //         info.SetActive(true);
    //     }
    //     else if (distance >= 7 && droneDistance >= 7 && info.activeSelf){
    //         info.SetActive(false);
    //     }

    //     if (Input.GetKey(KeyCode.E) && droneDistance < 7 && droneOnGround.activeSelf) {
    //         gunArm.SetActive(true);
    //         crosshair.SetActive(true);
    //         info.SetActive(false);
    //         slasherArm.SetActive(false);
    //     }
    // //rubble info

    //     if (rubble != null) {
    //         atkDistance = Vector3.Distance(player.position, rubble.transform.position);
    //         if (atkDistance < 4 && slasherArm.activeSelf && !atkInfo.activeSelf && rubble.activeSelf) {
    //             atkInfo.SetActive(true);
    //         }
    //         else if ((atkDistance >= 4 && atkInfo.activeSelf) || !rubble.activeSelf){
    //             atkInfo.SetActive(false);
    //         }
    //     }

    // //crate info

    //     if (crate != null) {
    //         holdDistance = Vector3.Distance(player.position, crate.transform.position);
    //         if (holdDistance < 15 && !holdInfo.activeSelf && !holdShown) {
    //             holdInfo.SetActive(true);
    //             StartCoroutine(waitForSeconds(3f));
    //         }
    //     }

    // //misc

    //     // if (creep == null) {
    //     //     doorTwo.Translate(5f, 0f, 0f);
    //     // }

    // //keypad info

    //     keyDistance = Vector3.Distance(player.position, keypad.transform.position);
    //     if (keyDistance < 8 && !keyInfo.activeSelf && !kp.returnLayerOn()) {
    //         keyInfo.SetActive(true);
    //     }
    //     else if ((keyDistance > 8 && keyInfo.activeSelf) || kp.returnLayerOn()) {
    //         keyInfo.SetActive(false);
    //     }
        
    // }

    //     IEnumerator waitForSeconds(float duration)
    //     {
    //         yield return new WaitForSeconds(duration);
    //         if (holdInfo.activeSelf) {
    //             holdInfo.SetActive(false);
    //             holdShown = true;
    //         }
    //     }
    

    
}