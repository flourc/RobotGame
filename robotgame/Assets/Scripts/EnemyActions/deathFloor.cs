using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathFloor : MonoBehaviour
{
    private Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOff() {
        myRenderer.material.color = Color.black;
        myRenderer.material.DisableKeyword("_EMISSION");
        enemyAttack ea = GetComponent<enemyAttack>();
        ea.enabled = false;
    }
}
