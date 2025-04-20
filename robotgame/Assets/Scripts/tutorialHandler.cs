using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialHandler : MonoBehaviour
{
    public Transform player;
    public GameObject movementDir;
    public GameObject grabDir;
    public DoorNextScene door;
    public bool gshown;
    public GameHandler gh;

    // Start is called before the first frame update
    void Start()
    {
        movementDir.SetActive(true);
        grabDir.SetActive(false);

        gshown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > -30 && movementDir.activeSelf) {
            movementDir.SetActive(false);
        }

        if (player.position.z > -8 && !grabDir.activeSelf) {
            grabDir.SetActive(true);
            gshown = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && gshown && door.inRadius) {
            door.GoNextScene();
        }
    }

}
