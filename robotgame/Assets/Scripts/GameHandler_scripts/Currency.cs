using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Currency : MonoBehaviour
{
    public static Currency instance;
    public TMP_Text CurrencyText;
    private int playerCurrency = 0;


    void Awake()
    {

        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        CurrencyText.text = playerCurrency.ToString();
    }

    public void AddCurrency(int currency){
        playerCurrency += currency;
        CurrencyText.text = playerCurrency.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
