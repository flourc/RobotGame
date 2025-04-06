using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadNumber : MonoBehaviour
{
    public GameObject [] numbers = new GameObject[11];
    // Start is called before the first frame update
    void Start()
    {
        setNA();
    }

    // Update is called once per frame
    void Update()
    {
        
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
