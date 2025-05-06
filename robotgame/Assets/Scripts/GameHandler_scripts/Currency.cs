using System.Collections;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    public static Currency instance;
    public TMP_Text CurrencyText;
    public int playerCurrency = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Start()
    {
        // Wait until PlayerStatsCollector is available
        while (PlayerStatsCollector.instance == null)
        {
            yield return null;
        }

        // Get initial currency from stats
        playerCurrency = PlayerStatsCollector.instance.GetCurrency();
        UpdateCurrencyText();

        Debug.Log("Currency initialized with: " + playerCurrency);
    }

    public void AddCurrency(int currency)
    {
        playerCurrency += currency;
        UpdateCurrencyText();

        if (PlayerStatsCollector.instance != null)
        {
            PlayerStatsCollector.instance.SetCurrency(playerCurrency);
        }
    }

    public void SetCurrency(int newAmount)
    {
        playerCurrency = newAmount;
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        if (CurrencyText != null)
        {
            CurrencyText.text = playerCurrency.ToString();
        }
        else
        {
            Debug.LogWarning("CurrencyText is not assigned in the inspector!");
        }
    }
}
