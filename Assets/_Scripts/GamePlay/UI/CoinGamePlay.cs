using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinGamePlay : Singleton<CoinGamePlay>
{
    public TextMeshProUGUI coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = SaveSystem.GetCoin().ToString();
    }

    // Update is called once per frame
    public void UpdateCoinText()
    {
        coinText.text = SaveSystem.GetCoin().ToString();
    }
}
