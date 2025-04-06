using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{

    public enum digits 
        { zero, uno, two, three, four, five, six, seven, eight, nine, none };


    public KeypadNumber num1, num2, num3, num4;
    public digits [] nums = 
                    { digits.none, digits.none, digits.none, digits.none };

    public digits [] correct_combo = new digits [4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++) {
            nums[i] = digits.none;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Zero()
    {
        Debug.Log("zero");
        inputNum(digits.zero);
    }
    public void Uno()
    {
        Debug.Log("one");
        inputNum(digits.uno);
    }
    public void Two()
    {
        Debug.Log("two");
        inputNum(digits.two);
    }
    public void Three()
    {
        Debug.Log("three");
        inputNum(digits.three);
    }
    public void Four()
    {
        Debug.Log("four");
        inputNum(digits.four);
    }
    public void Five()
    {
        Debug.Log("five");
        inputNum(digits.five);
    }
    public void Six()
    {
        Debug.Log("six");
        inputNum(digits.six);
    }
    public void Seven()
    {
        Debug.Log("seven");
        inputNum(digits.seven);
    }
    public void Eight()
    {
        Debug.Log("eight");
        inputNum(digits.eight);
    }
    public void Nine()
    {
        Debug.Log("nine");
        inputNum(digits.nine);
    }

    public void Clear()
    {
        Debug.Log("clear");
        for (int i = 0; i < 4; i++) {
            nums[i] = digits.none;
        }
        updateNums();
    }

    void inputNum(digits dig) 
    {
        if (nums[0] != digits.none) {
            return;
        }
        shift();
        nums[3] = dig;
        updateNums();
    }

    void shift()
    {
        nums[0] = nums[1];
        nums[1] = nums[2];
        nums[2] = nums[3];
    }

    void updateNums()
    {
        num1.switchDigit(digitToInt(nums[0]));
        num2.switchDigit(digitToInt(nums[1]));
        num3.switchDigit(digitToInt(nums[2]));
        num4.switchDigit(digitToInt(nums[3]));
    }

    int digitToInt(digits dig) 
    {
        switch (dig) {
            case digits.zero:   return 0;
            case digits.uno:    return 1;
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
