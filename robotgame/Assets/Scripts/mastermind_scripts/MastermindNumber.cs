using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MastermindNumber : MonoBehaviour
{
    public GameObject myBG;
    public KeypadNumber myNum;

    private Renderer myRendererBG;

    public Material goodBG, medBG, badBG;
    public Material goodNum, medNum, badNum;

    void Start()
    {
        myRendererBG = myBG.GetComponent<Renderer>();
    }

    void Update()
    {
        
    }

    public void SetGood()
    {
        myRendererBG.material = goodBG;
        myNum.SetNumColor(goodNum);
    }

    public void SetMed()
    {
        myRendererBG.material = medBG;
        myNum.SetNumColor(medNum);
    }

    public void SetBad()
    {
        myRendererBG.material = badBG;
        myNum.SetNumColor(badNum);
    }
}
