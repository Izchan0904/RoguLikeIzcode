using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText == null)
        {
            return;
        }

        if (CoinManager.Instance == null)
        {
            coinText.text = "Coin : 0";
            return;
        }

        coinText.text = "Coin : " + CoinManager.Instance.coin;
    }
}