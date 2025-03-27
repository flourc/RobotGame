using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandlerLimbs : MonoBehaviour
{
    public KeyCode[] myKeys;
    public RobotLimb[] myLimbs;
    public GameObject[] tempLimbs;
    public RobotLimb[] currLimbs;
    private int numLimbs;
    // Start is called before the first frame update
    void Start()
    {
        numLimbs = 2;
        tempLimbs = GameObject.FindGameObjectsWithTag("Limb");
        myLimbs = new RobotLimb[tempLimbs.Length];
        for (int i = 0; i < tempLimbs.Length; i++) 
        {
            myLimbs[i] = tempLimbs[i].GetComponent<RobotLimb>();
        }
        currLimbs = new RobotLimb[numLimbs];
        myKeys = new KeyCode[numLimbs];
        InstantiateLimbs();
    }

    // Update is called once per frame 
    void Update()
    {
        for (int i = 0; i < numLimbs; i++) 
        {
            if (Input.GetKeyDown(myKeys[i])) 
            {
                SwitchLimb(i);
            }
        }
    }

    private void SwitchLimb(int idx) 
    {
        for (int i = 0; i < numLimbs; i++) 
        {
            if (i == idx)
            {
                currLimbs[i].attached = true;
            } else {
                currLimbs[i].attached = false;
            }
        }
    }

    private void InstantiateLimbs()
    {
        for (int i = 0; i < myLimbs.Length && i < numLimbs; i++)
        {
            currLimbs[i] = myLimbs[i];
            myKeys[i] = currLimbs[i].myKey;
        }
    }
}
