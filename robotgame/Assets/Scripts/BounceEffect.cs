using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float bounceHeight = 0.25f;
    public float bounceSpeed = 2f;
    public float rotationSpeed = 45f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Float up and down using a sine wave
        float newY = startPos.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotate slowly around Y axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}

