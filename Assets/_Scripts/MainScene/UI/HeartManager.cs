using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    public TextMeshProUGUI heartText;
    public TextMeshProUGUI timeText;
    public Button addHeartButton;
    public TextMeshProUGUI numHeartInHeartImage;
    public TextMeshProUGUI nextHeartTime;
    public TextMeshProUGUI priceOfRefill;

    public BuyHeartPanel zeroHeartPanel;

    public static int priceOneHeart = 180;
    private int heartCount;
    public static int maxHeartCount = 5;
    public static int secondsToRecoverHeart = 1800;
    private int remainSeconds;
    private float timer = 0f; // Biến đếm thời gian trôi qua
    void Start()
    {
        heartCount = SaveSystem.GetHeart();

        remainSeconds = SaveSystem.GetRemainderSec();
        if (heartCount == -1)
        {
            heartCount = maxHeartCount;
        }

        if (heartCount < maxHeartCount)
        {
            string timeNow = DateTime.Now.ToString();
            int timeBetween = TimeCalculattion.CalculateTimeDifferenceInSeconds(timeNow, SaveSystem.GetTime());
            if (timeBetween >= remainSeconds)
            {
                if (remainSeconds != 0)
                {
                    heartCount++;
                }
                heartCount += (timeBetween - remainSeconds) / secondsToRecoverHeart;
                remainSeconds = secondsToRecoverHeart - (timeBetween - remainSeconds) % secondsToRecoverHeart;
            }
            else
            {
                remainSeconds = remainSeconds - timeBetween;
            }
        }
        heartCount = Mathf.Min(heartCount, maxHeartCount);
        
        numHeartInHeartImage.text = heartCount.ToString();
        priceOfRefill.text = (priceOneHeart * (maxHeartCount - heartCount)).ToString();
        heartText.text = heartCount.ToString();

        SaveSystem.SaveHeart(heartCount);

        // Cập nhật timeText ban đầu
        if (heartCount < maxHeartCount)
        {
            addHeartButton.interactable = true;
            timeText.text = TimeCalculattion.ConvertSecondsToMinutesSeconds(remainSeconds);
        }
        else
        {
            addHeartButton.interactable = false;
            timeText.text = "MAX";
        }
    }

    private void Update()
    {
        heartCount = SaveSystem.GetHeart();

        /////////////////
        //remainSeconds = SaveSystem.GetRemainderSec();








        /////////////////
        SaveSystem.SaveTime();
        if (heartCount < maxHeartCount)
        {
            // Đếm thời gian trôi qua
            timer += Time.deltaTime;

            // Nếu đã trôi qua 1 giây
            if (timer >= 1f)
            {
                timer = 0f; // Reset timer
                remainSeconds--; // Giảm thời gian còn lại
                SaveSystem.SaveRemainderSec(remainSeconds);
                // thời gian còn lại <= 0, những gì thay đổi khi thêm mạng thì để đây
                if (remainSeconds <= 0)
                {
                    heartCount++;
                    remainSeconds = secondsToRecoverHeart; // Reset thời gian đếm ngược
                    SaveSystem.SaveRemainderSec(remainSeconds);
                    // Đảm bảo heartCount không vượt quá maxHeartCount
                    heartCount = Mathf.Min(heartCount, maxHeartCount);
                    numHeartInHeartImage.text = heartCount.ToString();
                    priceOfRefill.text = (priceOneHeart * (maxHeartCount - heartCount)).ToString();
                    heartText.text = heartCount.ToString();
                    SaveSystem.SaveHeart(heartCount);
                }

                // Cập nhật timeText, những gì thay đổi thường xuyên thì để đây
                timeText.text = TimeCalculattion.ConvertSecondsToMinutesSeconds(remainSeconds);
                nextHeartTime.text = timeText.text;

            }
        }
        if (heartCount >= maxHeartCount)
        {
            heartText.text = heartCount.ToString();
            addHeartButton.interactable = false;
            timeText.text = "MAX";
            if (zeroHeartPanel.buyHeartPanelContainer.activeSelf)
            {
                zeroHeartPanel.OnClosePanelButtonClick();
            }
            return;
        }
    }

}
