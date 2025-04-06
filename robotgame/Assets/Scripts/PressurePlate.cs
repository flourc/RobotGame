using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Material newMaterial;  // Material to apply when object is on the plate
    public Material originalMaterial; // Original material of the plate

    private Renderer plateRenderer; // Renderer of the plate

    private void Start()
    {
        plateRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Change the material of the plate when an object is on it
        if (plateRenderer != null)
        {
            plateRenderer.material = newMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Revert the material of the plate back to the original when the object leaves
        if (plateRenderer != null)
        {
            plateRenderer.material = originalMaterial;
        }
    }
}