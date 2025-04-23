using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastermind : MonoBehaviour
{

    public GameHandler handler;

    public const int NUM_TRIES = 6;
    private const int NUM_DIGITS = 4;
    public enum digits 
        { zero, one, two, three, four, five, six, seven, eight, nine, none };
    public digits [] correct_code = new digits[NUM_DIGITS];
    public MastermindTry [] tries = new MastermindTry[NUM_TRIES];

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
        for (int i = 0; i < NUM_DIGITS; i++) {
            tries[tryNum].SetDigit(myKeypad.nums[i], i);
        }
        myKeypad.Clear();
        ValidateLatest();
        tryNum++;
        if (got_it) {
            // do something here
            // yay they got the code!!
        } else if (tryNum >= NUM_TRIES) {
            // do something else here
            // they didn't get it in six, make them fight something
        } 
    }


    public void ValidateLatest()
    {
        tries[tryNum].ValidateAll(correct_code);
        got_it = tries[tryNum].allGood;
    }

}
