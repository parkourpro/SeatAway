using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoreTimeButtonController : MonoBehaviour
{
    public TimeController timeController;
    public Button moreTimeButton;
    public TextMeshProUGUI moreTimeCountText;
    private int moreTimeCount = 2;

    private void Start()
    {
        moreTimeButton.onClick.AddListener(OnMoreTimeButtonClick);
        moreTimeCount = 2;
        UpdateHelpCountUI();
    }

    void OnMoreTimeButtonClick()
    {
        if (moreTimeCount <= 0)
        {
            Debug.Log("Hết lượt trợ giúp tăng thời gian");
            return;
        }
        //Debug.Log(1);
        moreTimeCount--;
        UpdateHelpCountUI();
        timeController.AddTime(10);
    }

    private void UpdateHelpCountUI()
    {
        if (moreTimeCountText != null)
        {
            moreTimeCountText.text = moreTimeCount.ToString();
        }
    }
}
