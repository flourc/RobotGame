using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialHandler : MonoBehaviour
{
    public Transform player;
    public GameObject movementDir;
    public GameObject grabDir;
    public bool gshown;
    public GameObject nextButton;
    public GameHandler gh;

    // Start is called before the first frame update
    void Start()
    {
        movementDir.SetActive(true);
        grabDir.SetActive(false);
        nextButton.SetActive(false);

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

        if (Input.GetKeyDown(KeyCode.N) && gshown) {
            gh.levelTwoTemp();
        }
    }

    // void closeMPanel() {
    //     movementDir.SetActive(false);
    // }

    // void closeGPanel() {
    //     grabDir.SetActive(false);
    // }
}
