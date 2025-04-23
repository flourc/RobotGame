using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using digits = Mastermind.digits;


// NOTE TO SELF: keypad code is 2602

public class Keypad : MonoBehaviour
{
    public GameHandler handler;

    public KeypadNumber num1, num2, num3, num4;
    public digits [] nums = 
                    { digits.none, digits.none, digits.none, digits.none };

    public digits [] correct_combo = new digits [4];

    void Start()
    {
        for (int i = 0; i < 4; i++) {
            nums[i] = digits.none;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            One();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Two();
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            Three();
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            Four();
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            Five();
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            Six();
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            Seven();
        } else if (Input.GetKeyDown(KeyCode.Alpha8)) {
            Eight();
        } else if (Input.GetKeyDown(KeyCode.Alpha9)) {
            Nine();
        } else if (Input.GetKeyDown(KeyCode.Alpha0)) {
            Zero();
        } else if (Input.GetKeyDown(KeyCode.Backspace)) {
            Clear();
        } //else if (Input.GetKeyDown(KeyCode.Return)) {
        //    Check();
        //}
    }

    public void Zero()
    {
        inputNum(digits.zero);
    }
    public void One()
    {
        inputNum(digits.one);
    }
    public void Two()
    {
        inputNum(digits.two);
    }
    public void Three()
    {
        inputNum(digits.three);
    }
    public void Four()
    {
        inputNum(digits.four);
    }
    public void Five()
    {
        inputNum(digits.five);
    }
    public void Six()
    {
        inputNum(digits.six);
    }
    public void Seven()
    {
        inputNum(digits.seven);
    }
    public void Eight()
    {
        inputNum(digits.eight);
    }
    public void Nine()
    {
        inputNum(digits.nine);
    }

    public void Clear()
    {
        for (int i = 0; i < 4; i++) {
            nums[i] = digits.none;
        }
        updateNums();
    }

    public void Check()
    {
        bool all_good = true;
        for (int i = 0; i < 4; i++) {
            all_good = all_good && (nums[i] == correct_combo[i]);
        }
        if (all_good) {
            print("passed");
            handler.SendMessage("MainMenu");
        } else {
            print("failed");
            Clear();
            handler.SendMessage("DeactivateLayers");
        }
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
