using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadNumber : MonoBehaviour
{
    public GameObject [] numbers = new GameObject[11];
    private Renderer [] myRenderers;
    // Start is called before the first frame update
    void Start()
    {
        setNA();
        myRenderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumColor(Material color)
    {
        for (int i = 0; i < 11; i++) {
            myRenderers[i].material = color;
        }
    }

    public void switchDigit(int digit) 
    {
        for (int i = 0; i < 11; i++) {
            numbers[i].SetActive(digit == i);
        }
    }

    public void setNA()
    {
        switchDigit(10);
    }

}
