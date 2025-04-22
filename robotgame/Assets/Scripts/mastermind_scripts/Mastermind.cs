using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastermind : MonoBehaviour
{
    public enum digits 
        { zero, one, two, three, four, five, six, seven, eight, nine, none };
    public digits [] correct_code = new digits[4];
    public MastermindTry [] tries = new MastermindTry[4];

    public int tryNum;
    
    void Start()
    {
        tryNum = 0;
    }

    
    void Update()
    {
        
    }


    public void ValidateLatest()
    {
        tries[tryNum].ValidateAll(correct_code);
    }

}
