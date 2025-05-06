using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressureDoor : MonoBehaviour
{
    public bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PressurePlate.open && !doorOpen) {
            transform.Translate(0f, 6f, 0f);
            doorOpen = true;
        }
        else if (!PressurePlate.open && doorOpen){
            transform.Translate(0f, -6f, 0f);
            doorOpen = false;
        }
    }
}
