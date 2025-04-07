using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadUI : MonoBehaviour
{
    public GameObject keypadObj;
    public GameObject keypadUI;
    public float keypadInteractRadius;
    private bool keypadActive;
    // Start is called before the first frame update
    void Start()
    {
        KeypadOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerNearby() && !keypadActive) {
            KeypadOn();
        }

        if (keypadActive && Input.GetKeyDown(KeyCode.Escape)) {
            KeypadOff();
        }
    }

    void KeypadOn()
    {
        keypadActive = true;
        gameObject.SendMessage("SetOtherUIActive");
        keypadUI.SetActive(true);
    }

    void KeypadOff()
    {
        gameObject.SendMessage("SetOtherUIInactive");
        keypadActive = false;
        keypadUI.SetActive(false);
    }

    bool playerNearby()
    {
        Transform player = 
                    GameObject.FindWithTag("Player").GetComponent<Transform>();
        Vector3 playerXZ = Vector3.ProjectOnPlane(player.position, Vector3.up);
        Transform keypadLoc = keypadObj.GetComponent<Transform>();
        Vector3 keypadXZ = 
                        Vector3.ProjectOnPlane(keypadLoc.position, Vector3.up);
        return (Vector3.Distance(playerXZ, keypadXZ) <= keypadInteractRadius);
    }
}
