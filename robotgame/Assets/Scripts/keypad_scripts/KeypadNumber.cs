using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadNumber : MonoBehaviour
{
    public GameObject [] numbers = new GameObject[11];
    private Image [] myImages;
    // Start is called before the first frame update
    void Start()
    {
        myImages = GetComponentsInChildren<Image>();
        setNA();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumColor(Color color)
    {
        for (int i = 0; i < 11; i++) {
            myImages[i].color = color;
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
