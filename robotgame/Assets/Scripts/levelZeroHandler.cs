using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelZeroHandler : MonoBehaviour
{

    public GameObject slasherOnGround;
    public GameObject gunArm;
    public GameObject slasherArm;
    public float distance;
    public GameObject info;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        gunArm.SetActive(false);
        slasherArm.SetActive(false);
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
            // audios.Play();
        }
    }
}
