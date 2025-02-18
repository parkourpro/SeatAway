using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public TextMeshProUGUI rewardCoin;
    public TextMeshProUGUI rewardCoinx2;
    public Button continueButton;

    private int reward;
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GridManager.Instance.doneLevelData!="");
        int customerCount = GridManager.Instance.levelData.customers.Count;
        reward = customerCount * 12;
        rewardCoin.text = reward.ToString();
        rewardCoinx2.text = (reward * 2).ToString();
        continueButton.onClick.AddListener(OnContinueButtonClick);
    }

    void OnContinueButtonClick()
    {
        SaveSystem.SaveCoin(SaveSystem.GetCoin() + reward);
        SceneManager.LoadScene("MainScene");
    }

}
