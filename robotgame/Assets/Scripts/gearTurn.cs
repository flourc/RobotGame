using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gearTurn : MonoBehaviour
{

    public bool clockwise;
    public bool idle;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("still", idle);
        anim.SetBool("clockwise", clockwise);
        anim.SetBool("cclockwise", !clockwise);
    }
}
