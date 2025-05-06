using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelZeroHandler : MonoBehaviour
{


    public GameObject slasherOnGround;
    public GameObject gunArm;
    public GameObject slasherArm;
    public float distance;
    public GameObject info; //arminfo
    public Transform player;
    public GameObject doorInfo;
    public GameObject WASDInfo;
    public GameObject atkInfo;

    public bool atkshown = false;

    public DoorNextScene dns;

    // Start is called before the first frame update
    void Start()
    {
        WASDInfo.SetActive(true);
        gunArm.SetActive(false);
        slasherArm.SetActive(false);
        doorInfo.SetActive(false);
        atkInfo.SetActive(false);

        StartCoroutine(Wait(WASDInfo));

    }

    // Update is called once per frame
    void Update()
    {
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
            atkInfo.SetActive(true);
            StartCoroutine(Wait(atkInfo));
            // audios.Play();
        }

        if (dns.inRadius) {
            doorInfo.SetActive(true);
            dns.Unlock();
        }
        else {
            doorInfo.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && dns.inRadius) {
            dns.GoNextScene();
        }


    }

    IEnumerator Wait(GameObject panel) {
        yield return new WaitForSeconds(5);
        panel.SetActive(false);
    }




}
