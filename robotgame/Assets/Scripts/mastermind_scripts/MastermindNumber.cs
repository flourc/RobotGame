using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MastermindNumber : MonoBehaviour
{
    public KeypadNumber myNum;

    public GameObject neutralBG, goodBG, medBG, badBG;
    public Color goodNum, medNum, badNum;

    void Start()
    {
        DeactivateAllBut(neutralBG);
    }

    void Update()
    {
        
    }

    public void SetGood()
    {
        DeactivateAllBut(goodBG);
        myNum.SetNumColor(goodNum);
    }

    public void SetMed()
    {
        DeactivateAllBut(medBG);
        myNum.SetNumColor(medNum);
    }

    public void SetBad()
    {
        DeactivateAllBut(badBG);
        myNum.SetNumColor(badNum);
    }

    void DeactivateAllBut(GameObject ex)
    {
        neutralBG.SetActive(false);
        goodBG.SetActive(false);
        medBG.SetActive(false);
        badBG.SetActive(false);

        ex.SetActive(true);
    }
    
}
