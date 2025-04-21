using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using digits = Mastermind.digits;

public class MastermindTry : MonoBehaviour
{
    public digits [] myDigits = 
                    { digits.none, digits.none, digits.none, digits.none };

    public MastermindNumber [] myNums = new MastermindNumber[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Validate(int idx, digits [] code)
    {
        if (code[idx] == myDigits[idx]) {
            myNums[idx].SetGood();
        } else if (AnyGood(myDigits[idx], code)) {
            myNums[idx].SetMed();
        } else {
            myNums[idx].SetBad();
        }
    }

    public void ValidateAll(digits [] code)
    {
        for (int i = 0; i < 4; i++) {
            Validate(i, code);
        }
    }

    bool AnyGood(digits dig, digits [] code) {
        bool acc = false;
        for (int i = 0; i < 4; i++) {
            acc = (acc || dig == code[i]);
        }
        return acc;
    }

}

