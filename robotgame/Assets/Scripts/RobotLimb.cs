using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLimb : MonoBehaviour
{
    public enum Limb
    {
        ArmR, 
        ArmL, 
        Legs,
        Eyes
    }

    public bool attached;
    public Limb limb;
    private Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rend.enabled = attached;
    }
}
