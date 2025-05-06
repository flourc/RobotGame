using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelThreeHandler : MonoBehaviour
{
    public GameObject sword;
    public DoorNextScene door;
    public GameObject boss;
    public GameObject upgrade;

    // public GameObject doorInfo;
    public GameObject equipInfo;
    public GameObject interactInfo;
    public GameObject masterMindInfo;
    public GameObject mastermind;

    public float distance;
    public float doorDistance;
    public Transform player;

    

    // Start is called before the first frame update
    void Start()
    {
        sword.SetActive(false);
        equipInfo.SetActive(false);
        masterMindInfo.SetActive(false);
        interactInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (upgrade != null) {
             distance = Vector3.Distance(player.position, upgrade.transform.position);

            if (distance < 7 && !equipInfo.activeSelf) {
                equipInfo.SetActive(true);
            }
            else if (distance >= 7) {
                equipInfo.SetActive(false);
            }
        }
        else {
            equipInfo.SetActive(false);
        }


        if (boss == null) {
            door.Unlock();
        }

        doorDistance = Vector3.Distance(player.position, mastermind.transform.position);

        if (doorDistance<7 && !masterMindInfo.activeSelf && mastermind.activeSelf) {
            masterMindInfo.SetActive(true);
        }
        else if ((doorDistance > 7 || !mastermind.activeSelf) && masterMindInfo.activeSelf) {
            masterMindInfo.SetActive(false);
        }


        if (door.inRadius) {
            interactInfo.SetActive(true);
        }
        else if (interactInfo.activeSelf) {
            interactInfo.SetActive(false);
        }
        if (!door.locked && door.inRadius && Input.GetKeyDown(KeyCode.E)) {
            door.GoNextScene();
        }

    }
}
