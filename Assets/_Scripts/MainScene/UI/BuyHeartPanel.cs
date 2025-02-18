using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BuyHeartPanel : MonoBehaviour
{
    public GameObject buyHeartPanelContainer;
    public GameObject buyHeartPanel;
    public Button addHeartButton;
    public Button closeButton;
    public Image heartImage;
    public Image clockWise;
    public Button refillHeart;
    public TextMeshProUGUI numberCoinText;

    private CanvasGroup zeroHeartPanelContainerCanvasGroup;
    private Vector3 originalScale;
    private Sequence heartBeatSequence;
    private Tween clockWiseRotationTween;

    void Start()
    {
        buyHeartPanelContainer.SetActive(false);
        addHeartButton.onClick.AddListener(OnAddHeartButtonClick);
        closeButton.onClick.AddListener(OnClosePanelButtonClick);
        refillHeart.onClick.AddListener(OnRefillHeartButtonClick);

        zeroHeartPanelContainerCanvasGroup = buyHeartPanelContainer.GetComponent<CanvasGroup>();
        if (zeroHeartPanelContainerCanvasGroup == null)
        {
            zeroHeartPanelContainerCanvasGroup = buyHeartPanelContainer.AddComponent<CanvasGroup>();
        }

        originalScale = buyHeartPanel.transform.localScale;
        InitializeHeartBeatEffect();
    }

    void InitializeHeartBeatEffect()
    {
        heartBeatSequence = DOTween.Sequence();
        heartBeatSequence.Append(heartImage.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.8f).SetEase(Ease.InOutBack));
        heartBeatSequence.Append(heartImage.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.InOutBack));
        heartBeatSequence.AppendInterval(0.5f);
        heartBeatSequence.SetLoops(-1, LoopType.Restart);
    }

    void OnAddHeartButtonClick()
    {
        ShowZeroHeartPanel();
    }

    public void ShowZeroHeartPanel()
    {
        zeroHeartPanelContainerCanvasGroup.alpha = 0f;
        buyHeartPanel.transform.localScale = Vector3.zero;
        buyHeartPanelContainer.SetActive(true);

        zeroHeartPanelContainerCanvasGroup.DOFade(1f, 0.3f).SetUpdate(true);
        buyHeartPanel.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);

        heartBeatSequence.Play();
        StartClockWiseRotation();
    }

    void StartClockWiseRotation()
    {
        if (clockWiseRotationTween != null && clockWiseRotationTween.IsActive())
        {
            clockWiseRotationTween.Kill(); // Hủy tween cũ nếu nó đang chạy
        }

        clockWiseRotationTween = clockWise.transform.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    void OnRefillHeartButtonClick()
    {
        int currentCoin = SaveSystem.GetCoin();
        int price = HeartManager.priceOneHeart * (HeartManager.maxHeartCount - SaveSystem.GetHeart());
        if(currentCoin < price)
        {
            Debug.Log("Không đủ tiền");
            OnClosePanelButtonClick();
            return;
        }
        SaveSystem.SaveCoin(currentCoin - price);
        numberCoinText.text = (currentCoin - price).ToString();

        SaveSystem.SaveHeart(HeartManager.maxHeartCount);
        OnClosePanelButtonClick();
    }


    public void OnClosePanelButtonClick()
    {
        HideZeroHeartPanel();
    }

    void HideZeroHeartPanel()
    {
        zeroHeartPanelContainerCanvasGroup.DOFade(0f, 0.5f).SetUpdate(true)
            .OnComplete(() => {
                buyHeartPanelContainer.SetActive(false);
            });

        buyHeartPanel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).SetUpdate(true);

        heartBeatSequence.Pause();

        if (clockWiseRotationTween != null && clockWiseRotationTween.IsActive())
        {
            clockWiseRotationTween.Kill(); // Hủy tween khi ẩn panel
        }
    }

    void OnDestroy()
    {
        DOTween.KillAll();
    }
}