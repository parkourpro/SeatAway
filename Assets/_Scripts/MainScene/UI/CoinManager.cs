using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coinCount;
    void Start()
    {
        //SaveSystem.SaveCoin(50000);
        coinCount = SaveSystem.GetCoin();
        if(coinCount == -1)
        {
            coinCount = 900;
            SaveSystem.SaveCoin(coinCount);
        }
        coinText.text = coinCount.ToString();
    }

}
