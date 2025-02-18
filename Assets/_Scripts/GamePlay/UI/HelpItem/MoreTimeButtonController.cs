using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoreTimeButtonController : MonoBehaviour
{
    //public TimeController timeController;
    public Button moreTimeButton;
    public TextMeshProUGUI helpCountText;
    public BuyHelpItem buyHelpItem;
    public FreezeTimeImage freezeTimeImage;
    public Image filledImage;

    public static bool isFreezeTime;
    private int timeFreezeDuration = 10;


    private int helpCount;

    private void Start()
    {
        moreTimeButton.onClick.AddListener(OnMoreTimeButtonClick);
        helpCount = 2;
        UpdateHelpCountUI();
        isFreezeTime = false;
        filledImage.fillAmount = 0;
    }

    void OnMoreTimeButtonClick()
    {
        // Nếu người chơi chưa kéo ghế nào (thời gian chưa chạy) thì không dùng được
        if (!MoveSeat.isDragFirstSeat)
        {
            return;
        }
        // Nếu đang trong thời gian hiệu lực thì không dùng được
        if (isFreezeTime)
        {
            return;
        }
        if (TimeController.hasWon)
        {
            return;
        }


        if (helpCount <= 0)
        {
            Debug.Log("Hết lượt trợ giúp tăng thời gian");
            buyHelpItem.ShowBuyHelpItemPanel(1);
            return;
        }
        helpCount--;
        UpdateHelpCountUI();
        //timeController.AddTime(10);
        StartCoroutine(TimeController.Instance.FreezeTime(timeFreezeDuration));
        StartCoroutine(freezeTimeImage.AnimateFreeze(timeFreezeDuration));
        StartCoroutine(RotateImg());
    }

    private void UpdateHelpCountUI()
    {
        helpCountText.text = helpCount.ToString();
        if (helpCount <= 0)
        {
            helpCountText.text = "+";
        }
    }

    public void AddHelpCount(int count)
    {
        helpCount += count;
        UpdateHelpCountUI();
    }

    IEnumerator RotateImg()
    {
        filledImage.fillAmount = 1;
        while (isFreezeTime)
        {
            filledImage.fillAmount -= (float)1 / timeFreezeDuration * Time.deltaTime;
            //Debug.Log(filledImage.fillAmount);
            yield return null;
        }
        //Debug.Log("Stop Rotate");
    }
}