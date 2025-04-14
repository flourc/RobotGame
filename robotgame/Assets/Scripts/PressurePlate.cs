using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Renderer plateRenderer; // Renderer of the plate
    public static bool open;

    private void Start()
    {
        open = false;

        plateRenderer = GetComponent<MeshRenderer>();
        plateRenderer.material.color = Color.red;
    }

    private void OnTriggerStay(Collider other)
    {
        // Change the material of the plate when an object is on it
        //Debug.Log("Object staying on plate: " + other.name);
        if (plateRenderer != null)
        {
            plateRenderer.material.color = Color.green;
            open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Revert the material of the plate back to the original when the object leaves
        //Debug.Log("Object left plate: " + other.name);
        if (plateRenderer != null)
        {
            plateRenderer.material.color = Color.red;
            open = false;
        }
    }
}