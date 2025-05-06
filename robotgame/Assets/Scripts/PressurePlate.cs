using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Renderer plateRenderer; // Renderer of the plate
    public static bool open;
    public GameObject plateInfo;
    public Transform player;
    public float distance;

    private void Start()
    {
        open = false;
        plateRenderer = GetComponent<MeshRenderer>();
        plateRenderer.material.EnableKeyword("_EMISSION");
        plateRenderer.material.color = Color.red;
        plateRenderer.material.SetColor("_EmissionColor", Color.red);

        plateInfo.SetActive(false);
    }

    void Update() {
        distance = Vector3.Distance(player.position, transform.position);
        if (!open && distance < 3 && !plateInfo.activeSelf) {
            plateInfo.SetActive(true);
        }
        else if (distance >= 3 && plateInfo.activeSelf){
            plateInfo.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Change the material of the plate when an object is on it
        //Debug.Log("Object staying on plate: " + other.name);

        if (plateRenderer != null)
        {
            plateRenderer.material.color = Color.green;
            plateRenderer.material.SetColor("_EmissionColor", Color.green);
            open = true;
            plateInfo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Revert the material of the plate back to the original when the object leaves
        //Debug.Log("Object left plate: " + other.name);
        if (plateRenderer != null)
        {
            plateRenderer.material.color = Color.red;
            plateRenderer.material.SetColor("_EmissionColor", Color.red);
            open = false;
            plateInfo.SetActive(false);
        }
    }
}