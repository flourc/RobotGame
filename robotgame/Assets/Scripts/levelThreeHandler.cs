using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelThreeHandler : MonoBehaviour
{
    public GameObject sword;
    public GameObject door;
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        sword.SetActive(false);
        door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null) {
            door.SetActive(true);
        }
    }
}
