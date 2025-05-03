using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using digits = Mastermind.digits;

public class MastermindTry : MonoBehaviour
{

    // abstract representation of digits
    public digits [] myDigits = 
                    { digits.none, digits.none, digits.none, digits.none };

    // displayed numbers
    public MastermindNumber [] myNums = new MastermindNumber[4];

    public bool allGood;
    public bool currGood;

    void Start()
    {
        allGood = false;
        currGood = true;
    }


    void Update()
    {
        
    }

    public void SetDigit(digits dig, int idx)
    {
        myDigits[idx] = dig;
        myNums[idx].myNum.switchDigit(digitToInt(dig));
        
    }

    void Validate(int idx, digits [] code)
    {
        if (code[idx] == myDigits[idx]) {
            myNums[idx].SetGood();
        } else if (AnyGood(myDigits[idx], code)) {
            myNums[idx].SetMed();
            currGood = false;
        } else {
            myNums[idx].SetBad();
            currGood = false;
        }
    }

    public void ValidateAll(digits [] code)
    {
        currGood = true;
        for (int i = 0; i < 4; i++) {
            Validate(i, code);
        }
        allGood = currGood;
    }

    public void Reset()
    {
        for (int i = 0; i < 4; i++) {
            myNums[i].Reset();
            myDigits[i] = digits.none;
            
        }
        allGood = false;
        currGood = true;
    }

    bool AnyGood(digits dig, digits [] code) {
        bool acc = false;
        for (int i = 0; i < 4; i++) {
            acc = (acc || dig == code[i]);
        }
        return acc;
    }

    int digitToInt(digits dig) 
    {
        switch (dig) {
            case digits.zero:   return 0;
            case digits.one:    return 1;
            case digits.two:    return 2;
            case digits.three:  return 3;
            case digits.four:   return 4;
            case digits.five:   return 5;
            case digits.six:    return 6;
            case digits.seven:  return 7;
            case digits.eight:  return 8;
            case digits.nine:   return 9;
            default: return 10;
        }
    }

}

