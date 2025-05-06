using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastermind : MonoBehaviour
{

    public GameHandler handler;

    public GameObject myDoor;

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
        RandomCode();
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
            myDoor.SetActive(false);
            handler.BroadcastMessage("DeactivateLayers");
        } else if (tryNum >= NUM_TRIES) {
            Reset();
        } 
    }


    public void ValidateLatest()
    {
        tries[tryNum].ValidateAll(correct_code);
        got_it = tries[tryNum].allGood;
    }

    public void Reset()
    {
        for (int i = 0; i < NUM_TRIES; i++) {
            tries[i].Reset();
        }
        tryNum = 0;
        got_it = false;
        RandomCode();
    }

    private void RandomCode()
    {
        int placeholder;
        for (int i = 0; i < NUM_DIGITS; i++) {
            placeholder = Random.Range(0, 10);

            switch (placeholder) {
                case 0: correct_code[i] = digits.zero; break;
                case 1: correct_code[i] = digits.one; break;
                case 2: correct_code[i] = digits.two; break;
                case 3: correct_code[i] = digits.three; break;
                case 4: correct_code[i] = digits.four; break;
                case 5: correct_code[i] = digits.five; break;
                case 6: correct_code[i] = digits.six; break;
                case 7: correct_code[i] = digits.seven; break;
                case 8: correct_code[i] = digits.eight; break;
                case 9: correct_code[i] = digits.nine; break;
                default: break;
            }
        }
    }

}
