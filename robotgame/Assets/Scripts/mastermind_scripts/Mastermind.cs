using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastermind : MonoBehaviour
{
    public enum digits 
        { zero, one, two, three, four, five, six, seven, eight, nine, none };
    public digits [] correct_code = new digits[4];
    public MastermindTry [] tries = new MastermindTry[4];

    public Keypad myKeypad;

    public int tryNum;
    public bool got_it;
    
    void Start()
    {
        tryNum = 0;
        got_it = false;
    }

    
    void Update()
    {
        
    }

    public void TransferTry()
    {
        if (tryNum == 3) {
            
        } else {
            for (int i = 0; i < 4; i++) {
                tries[tryNum].SetDigit(myKeypad.nums[i], i);
            }
            myKeypad.Clear();
            ValidateLatest();
            tryNum++;
            if (got_it) {
                // do something here
                // 
            }
        }
        
    }


    public void ValidateLatest()
    {
        tries[tryNum].ValidateAll(correct_code);
        got_it = tries[tryNum].allGood;
    }

}
